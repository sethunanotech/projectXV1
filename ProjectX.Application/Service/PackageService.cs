using AutoMapper;
using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.Package;
using ProjectX.Domain.Entities;
using ProjectX.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Service
{
    public class PackageService: IPackageService
    {
        private readonly IPackage _packageRepository;
        private readonly IMapper _mapper;
        private readonly ICryptography _cryptography;
        private readonly ICacheService _cacheService;
        private string _cacheKey = "ProjectX";

        public PackageService(IPackage packageRepository, IMapper mapper, ICryptography cryptography, ICacheService cacheService)
        {
            _packageRepository = packageRepository;
            _mapper = mapper;
            _cryptography = cryptography;
            _cacheService = cacheService;
        }

        public async Task<Package> AddPackage(PackageAddRequest packageAddRequest)
        {
            Package package =null;
            var packageData = _mapper.Map<Package>(packageAddRequest);
            var packageUrl =await _cryptography.SaveFile(packageAddRequest.File, packageAddRequest.Version,"");
            if(packageUrl !="")
            {
                packageData.Url = packageUrl;
                package = await _packageRepository.AddAsync(packageData);
                _cacheService.Remove(_cacheKey);
            }
            return package;
        }

        public async Task<Package> RemovePackage(Guid Id)
        {
            Package package=new Package();
            package = await _packageRepository.GetByIdAsync(Id);
            if (package != null)
            {
                await _packageRepository.RemoveByIdAsync(package.Id);
                _cacheService.Remove(_cacheKey);
            }
            return package;
        }
        public async Task<IEnumerable<GetPackageResponse>> GetAll()
        {
            var cacheData = _cacheService.TryGet<IEnumerable<GetPackageResponse>>(_cacheKey);
            if (cacheData != null)
            {
                return cacheData;
            }
            List<GetPackageResponse> listOfPackages = new List<GetPackageResponse>();
            var package = await _packageRepository.GetAllAsync();
            if (package.Count() >0)
            {
                 listOfPackages = _mapper.Map<List<GetPackageResponse>>(package);
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            _cacheService.Set<IEnumerable<GetPackageResponse>>(_cacheKey, listOfPackages, expirationTime);
            return listOfPackages;
        }

        public async Task<GetPackageResponse> GetByID(Guid ID)
        {
            var package = await _packageRepository.GetByIdAsync(ID);
            var getPackageById = _mapper.Map<GetPackageResponse>(package);
            return getPackageById;
        }
        
        public async Task<Package> UpdatePackage(PackageUpdateRequest packageUpdateRequest)
        {
            Package updatedPackage = new Package();
            var package = _mapper.Map<Package>(packageUpdateRequest);
            var existingPackage = await _packageRepository.GetByIdAsync(packageUpdateRequest.Id);
            if (existingPackage != null)
            { 
                package.CreatedOn= existingPackage.CreatedOn;
                package.CreatedBy= existingPackage.CreatedBy;
                var packageUrl = await _cryptography.SaveFile(packageUpdateRequest.File, packageUpdateRequest.Version,existingPackage.Url);
                if (packageUrl != "")
                {
                    package.Url= packageUrl;
                   updatedPackage = await _packageRepository.UpdateAsync(package);
                    _cacheService.Remove(_cacheKey);
                }
            }
            return updatedPackage;
        }

        public Task<bool> CheckCombinationprojectEntityExist(Guid entityID, Guid projectID)
        {
            var isprojectEntityExist = _packageRepository.CheckCombinationprojectEntityExist(entityID, projectID);
            return isprojectEntityExist;
        }
    }
}
