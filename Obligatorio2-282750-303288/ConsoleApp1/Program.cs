using System.Text;

internal class ImpresionCodigoFuenteSolucion
{
    /// <summary>
    /// Imprime los archivos de código con extensión .cs y las views .cshtml.
    /// Para agregar otro tipo de archivo simplemente invocar al método Imprimir
    /// indicando *.extensión
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Imprimir("*.cs", "fuentesCs.txt");
        Imprimir("*.cshtml", "views.txt");

    }
    /// <summary>
    /// Imprime los archivos de código fuente de la solución
    /// </summary>
    /// <param name="tipoArchivo">El nombre del archivo a imprimir.Para imprimir todos los de un tipo determinado usar "*.extensión" por ejemplo:  "*.cs"</param>
    /// <param name="nombreArchivoSalida">El nombre del archivo de texto donde quedarà el código fuente</param>
    /// <remarks>
    /// Este código funciona siempre que el archivo de la solución (.sln) esté en la raíz de la solución,
    /// es decir cuando todos los proyectos estàn en subcarpetas de la carpeta de la solución
    /// </remarks>
    private static void Imprimir(string tipoArchivo, string nombreArchivoSalida)
    {
        try
        {
            string raizSolucion = ObtenerRutaSolucion();
            var separador = "***********************************" + Environment.NewLine;

            var archivos = System.IO.Directory.GetFiles(raizSolucion, tipoArchivo, System.IO.SearchOption.AllDirectories);

            //se obtienen los archivos .cs excluyendo los que contienen código generado por el framework
            var resultado = archivos.Where(p => !p.Contains("Temporary")
            && !p.Contains("AssemblyInfo.cs")
            && !p.Contains("Program.cs")
                && !p.Contains("AssemblyAttributes")
                && !p.Contains(".g.cs"))
                .Select(path => new { Carpeta = path, Nombre = System.IO.Path.GetFileName(path), Contenido = System.IO.File.ReadAllText(path) })
                              .Select(info =>
                                  separador
                                + "Archivo: " + info.Nombre + Environment.NewLine
                                + "Carpeta: " + info.Carpeta + Environment.NewLine
                                + separador
                                + info.Contenido);


            var concatenado = string.Join(Environment.NewLine, resultado);
            File.WriteAllText(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())))
                + @$"\{nombreArchivoSalida}", concatenado, Encoding.UTF8);
        }
        catch (Exception algunError)
        {
            Console.WriteLine(algunError.Message);
        }
    }
    static string ObtenerRutaSolucion()
    {
        string directorioActivo = Directory.GetCurrentDirectory();

        Console.WriteLine("Directorio activo: " + directorioActivo);

        // Navega hacia arriba en la estructura de directorios hasta encontrar la carpeta de la solución
        DirectoryInfo directoryInfo = new DirectoryInfo(directorioActivo);

        while (directoryInfo != null && !DirectorioIncluye(directoryInfo, "*.sln"))
        {
            directoryInfo = directoryInfo.Parent;
        }

        if (directoryInfo != null)
        {
            return directoryInfo.FullName;
        }
        else
        {
            return string.Empty;
        }
    }

    static bool DirectorioIncluye(DirectoryInfo directory, string pattern)
    {
        return directory.GetFiles(pattern).Length > 0;
    }
}

