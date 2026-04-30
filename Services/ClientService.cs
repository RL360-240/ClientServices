using ClientServices.Model;
using Microsoft.Win32;
using System.Text.Json;

namespace ClientServices.Services
{
    public class ClientService
    {

    //Todo este código va dentro de public class ClienteService

    private readonly string _rutaArchivo = "clientes.json";

    public void Guardar(Clients nuevoCliente)
    {
        //Leer la lista actual (o crear una nueva si el archivo no existe)
        List<Clients> lista = new();
        if (File.Exists(_rutaArchivo))
        {
            var jsonExistente = File.ReadAllText(_rutaArchivo);
            lista = JsonSerializer.Deserialize<List<Clients>>(jsonExistente) ?? new();
        }

        //Agregar el nuevo cliente a la lista
        lista.Add(nuevoCliente);

        //Serializar el Json y escribir en el disco
        var opciones = new JsonSerializerOptions { WriteIndented = true };
        var nuevoJson = JsonSerializer.Serialize(lista, opciones);
        File.WriteAllText(_rutaArchivo, nuevoJson);
    }

    //Devuelve una lista de Clientes leidos desde el archivo
    public List<Clients> Leer()
    {
        if (!File.Exists(_rutaArchivo))
            return new List<Clients>();

        //El try sirve para "atrapar" errores y que no los muestre en la pantalla
        //Dentro del try (probar) se pone el código que vamos a ejecutar, si por alguna
        //razón ese código da error, por ejemplo en este caso si el archivo no existe
        //en lugar de dar un error en pantalla, se salta el error y ejecuta lo que está en 
        //en el catch
        try
        {
            var json = File.ReadAllText(_rutaArchivo);

            //los ?? se les llama operador de fusión NULL
            //Si la operación de la izquierda del ?? por algún razón es NULL
            //entonces devuelve lo que está después del ??
            //en este caso devuelve una lista vacía (pero no NULL para eviatar errores)
            return JsonSerializer.Deserialize<List<Clients>>(json) ?? new List<Clients>();
        }
        catch
        {
            return new List<Clients>();
        }
    }
    }
}
