using System.ComponentModel.DataAnnotations;

namespace Thu_y.Infrastructure.Utils.Constant
{
    public enum RoleType
    {
        [Display(Name = "Nhân viên")]
        Employee = 1,
        [Display(Name = "Quản lý")]
        Manager = 2
    }

    public enum AbattoirDetailStatus
    {
        [Display(Name = "Tồn kho")]
        Storing = 1,
        [Display(Name = "Không tồn kho")]
        Empty = 2
    }

    public enum SexType
    {
        Male = 1,
        Female = 2
    }

    public enum ReportType
    {
        A = 1,
        B = 2,
        C = 3,
    }
    public enum DataTypes
    {
        InputType = 1,
        DropDownListType = 2
    }
    public enum ControlTypes
    {
        TextControl = 1,
        TelControl = 2,
        DateControl = 3,
        EmailControl = 4,
        DatetimeLocalControl = 5,
        ImageControl = 6,
        PasswordControl = 7,
        NumberControl = 8
    }
}
