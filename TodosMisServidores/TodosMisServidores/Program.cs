namespace TodosMisServidores
{
    public class Program
    {
        public static void Main()
        {
            Servicio server = new Servicio(42069);
            server.conexion(1);
        }
    }
}