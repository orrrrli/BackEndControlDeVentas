namespace Presentacion.Models.Usuarios.Roles
{
    public class RolesViewModel
    {
        public int IdRol { get; set; }
        public string nombreRol { get; set; } = string.Empty;
        public string descripcionRol { get; set; } = string.Empty;
        public bool estado { get; set; }

    }
}
