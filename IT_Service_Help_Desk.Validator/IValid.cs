using IT_Service_Help_Desk.Dto;

namespace IT_Service_Help_Desk.Validator;

public interface IValid<T>
{
    (bool, string) IsValid(T obj);
}