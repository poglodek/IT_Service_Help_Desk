using AutoMapper;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Dto.Role;
using IT_Service_Help_Desk.Helpers;
using IT_Service_Help_Desk.IO.IServices;
using IT_Service_Help_Desk.Services.IServices;
using MySql.Data.MySqlClient;

namespace IT_Service_Help_Desk.Services.Services;

public class RoleServices : IRoleServices
{
    private readonly DatabaseHelper _helper;
    private readonly MySqlConnection _connection;
    private readonly DatabaseManagement _management;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public RoleServices(DatabaseManagement management,
        DatabaseHelper helper,
        DatabaseConnector connector,
        IMapper mapper,
        ILogger logger)
    {
        _helper = helper;
        _connection = connector.GetConnection();
        _management = management;
        _mapper = mapper;
        _logger = logger;
    }
    public RoleDto GetFirstRole()
    {
        try
        {
            var role = _management.GetResultFromQuery<Role>(@"Select * from roles;");
            return _mapper.Map<RoleDto>(role);
        }
        catch
        {
            _connection.Close();
            _logger.LogError("Cannot get first role");
            return null;
        }
    }

    public RoleDto GetRoleById(int id)
    {
        var role = _management.GetResultFromQuery<Role>(@$"Select * from roles where id={id};");
        return _mapper.Map<RoleDto>(role);
    }
}