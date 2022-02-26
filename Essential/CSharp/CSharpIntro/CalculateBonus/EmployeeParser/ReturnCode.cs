namespace EmployeeParser
{
    public enum ReturnCode
    {
        Success = 0,
        ErrorNotRequiredParameter = -1,
        ErrorInvalidRank = -4,
        ErrorRankNotExists = -5,
        ErrorInvalidDepartment = -6,
        ErrorDepartmentNotExists = -7,
        ErrorInvalidSalary = -8,
        ErrorInvalidSalaryPercent = -9,
        ErrorInvalidCoeff = -10,
        ErrorInvalidFormatRequiredParameter = -11,
        ErrorEmptySurname = -12,
        ErrorEmptyParameters = -13,
        ErrorInvalidEmployeeCodeFormat = -14
    }
}
