using ProjectX.Application.Usecases.DropDown;

namespace ProjectX.UnitTesting.Model
{
    public class DropDownMaster
    {
        public static List<DropDownModel> dropDownModelsList()
        {
            List<DropDownModel> dropDownModels = new List<DropDownModel>()
            {
                new DropDownModel
                {
                    Id= new Guid("0BB01FA1-A8B3-46F4-8CBB-32EC2755EFF0"),
                    Name="Clent"
                },
                new DropDownModel
                {
                    Id= new Guid("0AB01FA1-A8B3-46F4-8CBB-32EC2755EFF0"),
                    Name="User"
                }
            };
            return dropDownModels;
        }
    }
}
