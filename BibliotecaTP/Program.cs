using System;
using System.Data.SqlClient;
using System.Data;
using MySqlConnector;
using static Mysqlx.Crud.Order.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1;
using System.Diagnostics.CodeAnalysis;


namespace MySqlConsole
{


    
  


    class Biblioteca
    {


        string connectionString = "server=localhost; database=bibliotp; user=root;";
     
        static void Main(string[] args)
        {
            

            string connectionString = "server=localhost; database=bibliotp; user=root;";

            void CrearTablas()
            {
                string connectionString = "server=localhost; database=bibliotp; user=root;";

                string createTablesQuery = @"
         CREATE TABLE IF NOT EXISTS clientes(
           id INT NOT NULL AUTO_INCREMENT,
           nombre VARCHAR(30) NOT NULL,
           apellido VARCHAR(30) NOT NULL,
           dni VARCHAR(8) NOT NULL,
           telefono VARCHAR(40) NOT NULL,
           email VARCHAR(40) NOT NULL,
           creado_el TIMESTAMP DEFAULT NOW(),
           actualizado_el TIMESTAMP DEFAULT NOW(),
           estado TINYINT DEFAULT(1),
              PRIMARY KEY (id));
          
           CREATE TABLE IF NOT EXISTS genero(
           id INT NOT NULL AUTO_INCREMENT,
           genero VARCHAR(20) NOT NULl,
           PRIMARY KEY (id));
          
          CREATE TABLE IF NOT EXISTS libros(
           id INT NOT NULL AUTO_INCREMENT,
           nombre VARCHAR(40) NOT NULL,
           autor VARCHAR(40) NOT NULL,
           fecha_lanzamiento DATE,
           id_genero INT,
              creado_el TIMESTAMP DEFAULT NOW(),
           actualizado_el TIMESTAMP DEFAULT NOW(),
              FOREIGN KEY (id_genero) references genero(id),
           estado TINYINT DEFAULT(1),
           PRIMARY KEY (id));

           CREATE TABLE IF NOT EXISTS prestamo(
              id INT NOT NULL AUTO_INCREMENT,
              fecha_prestamo DATE,
              fecha_devolucion_estimada DATE,
              fecha_devolucion_real DATE,
              id_cliente INT,
              id_libro INT,
              CONSTRAINT fk_cliente
              FOREIGN KEY (id_cliente) REFERENCES clientes(id),  
              CONSTRAINT fk_libro
              FOREIGN KEY (id_libro) REFERENCES libros(id),
              PRIMARY KEY (id));
              ";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(createTablesQuery, conn);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Tablas creadas correctamente.");
                        Console.WriteLine("\nPresione cualquier tecla para iniciar el programa . . . ");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al crear las tablas: " + ex.Message);
                    }
                }
                Console.ReadLine();

            }

            CrearTablas();

            void crear_usuario()
            {
                string nombre;
                string apellido;
                string dni;
                string telefono;
                string email;

                Console.WriteLine("Ingresar nombre: ");
                nombre = Console.ReadLine();
                Console.WriteLine("Ingrese apellido: ");
                apellido = Console.ReadLine();
                Console.WriteLine("Ingrese dni: ");
                dni = Console.ReadLine();
                Console.WriteLine("Ingrese telefono de contacto: ");
                telefono = Console.ReadLine();
                Console.WriteLine("Ingrese email de contacto: ");
                email = Console.ReadLine();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "INSERT INTO clientes (nombre, apellido, dni, telefono, email) " + "VALUES (@nombre, @apellido, @dni, @telefono, @email)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@nombre", nombre);
                            command.Parameters.AddWithValue("@apellido", apellido);
                            command.Parameters.AddWithValue("@dni", dni);
                            command.Parameters.AddWithValue("@telefono", telefono);
                            command.Parameters.AddWithValue("@email", email);

                            int resultado = command.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                Console.WriteLine("\t\t\tRegistro agregado correctamente!....");
                            }
                            else
                            {
                                Console.WriteLine("\t\t\tError al iniciar el registro!...");
                            }


                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR en la conexión");
                    }
                }
            }

            void agregar_libro()
            {
                string nombre;
                string autor;
                string fecha_lanzamiento;
                string genero;

                Console.WriteLine("Ingresar nombre del libro: ");
                nombre = Console.ReadLine();
                Console.WriteLine("Ingrese autor del libro: ");
                autor = Console.ReadLine();
                Console.WriteLine("Fecha de lanzamiento (AA/MM/DD): ");
                fecha_lanzamiento = Console.ReadLine();
                tabla_genero();
                Console.WriteLine("\nInserte el ID del genero que desee:");
                genero = Console.ReadLine();


                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "INSERT INTO libros (nombre, autor, fecha_lanzamiento, id_genero) " + "VALUES (@nombre, @autor, @fecha_lanzamiento, @id_genero)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@nombre", nombre);
                            command.Parameters.AddWithValue("@autor", autor);
                            command.Parameters.AddWithValue("@fecha_lanzamiento", fecha_lanzamiento);
                            command.Parameters.AddWithValue("@id_genero", genero);


                            int resultado = command.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                Console.WriteLine("\t\t\tRegistro agregado correctamente!....");
                            }
                            else
                            {
                                Console.WriteLine("\t\t\tError al iniciar el registro!...");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR en la conexión");
                    }
                }
            }

            void tabla_usuarios()
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    int anchocolumna = 20;

                    try
                    {
                        conn.Open();
                        string query = "SELECT id, nombre, apellido, dni, telefono, email FROM clientes WHERE estado = 1";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("\t -------// TABLA DE USUARIOS //-------");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n ID |   NOMBRE   |  APELLIDO  |    DNI   |  TELEFONO    |         EMAIL             |");
                        Console.ResetColor();


                        int anchoColumna1 = 3;
                        int anchoColumna2 = 10;

                        int anchoColumna3 = 8;
                        int anchoColumna4 = 12;
                        int anchoColumna5 = 25;
                        int anchoseparador = 85;

                        while (reader.Read())
                        {
                            Console.WriteLine(new string('-', anchoseparador));

                            Console.WriteLine(
                                reader["id"].ToString().PadRight(anchoColumna1) + " | " +
                                reader["nombre"].ToString().PadRight(anchoColumna2) + " | " +
                                reader["apellido"].ToString().PadRight(anchoColumna2) + " | " +
                                reader["dni"].ToString().PadRight(anchoColumna3) + " | " +
                                reader["telefono"].ToString().PadRight(anchoColumna4) + " | " +
                                reader["email"].ToString().PadRight(anchoColumna5) + " |");
                            Console.WriteLine(new string('-', anchoseparador));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            void tabla_prestamo()
    
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
          
                    try
                    {
                        conn.Open();
                        string query = "SELECT id, fecha_prestamo, fecha_devolucion_estimada, fecha_devolucion_real, id_cliente, id_libro FROM prestamo";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("\t\t----------------// TABLA DE PRESTAMOS //----------------");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n ID |  FECHA DE PRESTAMO  | FECHA DE DEVOLUCION ESTIMADA | FECHA DE DEVOLUCION REAL |  ID_CLIENTE  | ID_LIBRO     |");
                        Console.ResetColor();

                        int anchoColumna1 = 3;
                        int anchoColumna2 = 19;
                        int anchoColumna3 = 28;
                        int anchoColumna5 = 24;
                        int anchoColumna4 = 12;
                        int anchoseparador = 115;


                        //Console.WriteLine(new string('-', anchoColumna1 + anchoColumna2 + anchoColumna2 + anchoColumna3 + anchoColumna4 + anchoColumna5));

                        while (reader.Read())
                        {
                            Console.WriteLine(new string('-', anchoseparador));

                            Console.WriteLine(
                                reader["id"].ToString().PadRight(anchoColumna1) + " | " +
                                reader["fecha_prestamo"].ToString().PadRight(anchoColumna2) + " | " +
                                reader["fecha_devolucion_estimada"].ToString().PadRight(anchoColumna3) + " | " +
                                reader["fecha_devolucion_real"].ToString().PadRight(anchoColumna5) + " | " +
                                reader["id_cliente"].ToString().PadRight(anchoColumna4) + " | " +
                                reader["id_libro"].ToString().PadRight(anchoColumna4) + " | " );

                            Console.WriteLine(new string('-', anchoseparador));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            void tabla_genero()
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    int anchocolumna = 20;

                    try
                    {
                        conn.Open();
                        string query = "SELECT id, genero FROM genero";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("----------// TABLA DE GENEROS//-----------");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n ID |  GENERO           |");
                        Console.ResetColor();

                        int anchoColumna1 = 3;
                        int anchoColumna2 = 18;
                        int anchoseparador = 25;

                        while (reader.Read())
                        {
                            Console.WriteLine(new string('-', anchoseparador));

                            Console.WriteLine(
                                reader["id"].ToString().PadRight(anchoColumna1) + " | " +
                                reader["genero"].ToString().PadRight(anchoColumna2) + "| ");

                            Console.WriteLine(new string('-', anchoseparador));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            void tabla_libros()
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    int anchocolumna = 20;

                    try
                    {
                        conn.Open();
                        string query = "SELECT id, nombre, autor, fecha_lanzamiento FROM libros WHERE estado=1";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("\t\t----------------// TABLA DE LIBROS //----------------");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n ID |            NOMBRE              |         AUTOR        |    FECHA DE EDICION   |");
                        Console.ResetColor();

                        int anchoColumna1 = 3;
                        int anchoColumna2 = 30;
                        int anchoColumna3 = 20;
                        int anchoseparador = 85; 

                        while (reader.Read())
                        {
                            Console.WriteLine(new string('-', anchoseparador));

                            Console.WriteLine(
                                reader["id"].ToString().PadRight(anchoColumna1) + " | " +
                                reader["nombre"].ToString().PadRight(anchoColumna2) + " | " +
                                reader["autor"].ToString().PadRight(anchoColumna3) + " | " +
                                reader["fecha_lanzamiento"].ToString().PadRight(anchoColumna3) + "  | ");
                            
                            Console.WriteLine(new string('-', anchoseparador));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            void actualizar_usuario()
            {
                string nombre;
                string apellido;
                string dni;
                string telefono;
                string email;
                string id;

                Console.WriteLine("Esta por modificar datos de un usuario, presione (ENTER) para continuar . . .");
                Console.ReadLine();
                Console.Clear();

                tabla_usuarios();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        Console.WriteLine("\nSeleccione el número de identificación del cliente: ");
                        id = Console.ReadLine();
                        Console.WriteLine("--> Modificar nombre: ");
                        nombre = Console.ReadLine();
                        Console.WriteLine("--> Modificar apellido: ");
                        apellido = Console.ReadLine();
                        Console.WriteLine("--> Modificar dni: ");
                        dni = Console.ReadLine();
                        Console.WriteLine("--> Modificar telefono: ");
                        telefono = Console.ReadLine();
                        Console.WriteLine("--> Modificar email: ");
                        email = Console.ReadLine();


                        string query = "UPDATE clientes SET telefono=@telefono, email=@email, nombre=@nombre, apellido=@apellido, dni=@dni, actualizado_el=NOW(0) WHERE id=@id";


                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@telefono", telefono);
                            cmd.Parameters.AddWithValue("@nombre", nombre);
                            cmd.Parameters.AddWithValue("@dni", dni);
                            cmd.Parameters.AddWithValue("@apellido", apellido);

                            int resultado = cmd.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                Console.WriteLine("\t\t\tUsuario actualizado con éxito!....");
                            }
                            else
                            {
                                Console.WriteLine("\t\t\tError al actualizar el usuario!...");
                            }
                        }

                        connection.Close();

                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            void agregar_genero()
            {
                string genero;

                Console.WriteLine("\rGénero que desea agregar: ");
                genero = Console.ReadLine();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "INSERT INTO genero (genero) " + "VALUES (@genero)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@genero", genero);



                            int resultado = command.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                Console.WriteLine("\t\t\tRegistro agregado correctamente!....");
                            }
                            else
                            {
                                Console.WriteLine("\t\t\tError al iniciar el registro!...");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR en la conexión");
                    }
                }
            }

            void borrar_usuario()
            {
                string modificar;
                string id;

                
                Console.WriteLine("Esta por dar de baja un usuario, presione (ENTER) para continuar . . .");
                Console.ReadLine();
                Console.Clear();
                tabla_usuarios();
                Console.WriteLine("\nSeleccione el número de identificación del usuario que desea dar de BAJA: ");
                id = Console.ReadLine();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "UPDATE clientes SET estado=0, actualizado_el=NOW(0) WHERE id=@id";


                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);

                            int resultado = cmd.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                Console.WriteLine("\t\t\tUsuario borrado con éxito!....");
                            }
                            else
                            {
                                Console.WriteLine("\t\t\tError al borrar el usuario!...");
                            }
                        }

                        connection.Close();

                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }

            void actualizar_libro()
            {
                string nombre;
                string autor;
                string id_genero;
                string fecha_lanzamiento;
                string id;


                Console.WriteLine("Esta por modificar datos de un usuario, presione (ENTER) para continuar . . ." +
                    "\n Si no quiere modificar algún dato agregue el mismo que ya tenía");
                Console.ReadLine();
                Console.Clear();
                tabla_libros();
                Console.WriteLine("\nSeleccione el número de identificación del libro que desea actualizar: ");
                id = Console.ReadLine();
                Console.WriteLine("--> Modificar nombre: ");
                nombre = Console.ReadLine();
                Console.WriteLine("--> Modificar autor: ");
                autor = Console.ReadLine();
                Console.WriteLine("--> Modificar fecha de lanzamiento: ");
                fecha_lanzamiento = Console.ReadLine();
                tabla_genero();
                Console.WriteLine("\n--> Seleccione el ID del nuevo género: ");
                id_genero = Console.ReadLine();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "UPDATE libros SET fecha_lanzamiento=@fecha_lanzamiento, autor=@autor, nombre=@nombre, id_genero=@id_genero, actualizado_el=NOW(0) WHERE id=@id";


                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@fecha_lanzamiento", fecha_lanzamiento);
                            cmd.Parameters.AddWithValue("@autor", autor);
                            cmd.Parameters.AddWithValue("@nombre", nombre);
                            cmd.Parameters.AddWithValue("@id_genero", id_genero);

                            int resultado = cmd.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                Console.WriteLine("\t\t\tLibro actualizado con éxito!....");
                            }
                            else
                            {
                                Console.WriteLine("\t\t\tError al actualizar el libro!...");
                            }
                        }

                        connection.Close();

                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            void borrar_libro()
            {

                string id;

                Console.WriteLine("Esta por borrar un libro de la base de datos, presione (ENTER) para continuar . . .");
                Console.ReadLine();
                Console.Clear();
                tabla_libros();
                Console.WriteLine("\nSeleccione el número de identificación del libro que desa BORRAR: ");
                id = Console.ReadLine();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "UPDATE libros SET estado=0, actualizado_el=NOW(0) WHERE id=@id";


                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);

                            int resultado = cmd.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                Console.WriteLine("\t\t\tLibro borrado con éxito!....");
                            }
                            else
                            {
                                Console.WriteLine("\t\t\tError al borrar el libro!...");
                            }
                        }

                        connection.Close();

                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            void actualizar_genero()
            {
                tabla_genero();
            }

            void crear_prestamo()
            {
                string fecha_prestamo;
                string fecha_dev_estimada;
                string id_libro;
                string id_cliente;
                string prestamo;
                string diferenciafecha;

                string clientes = "SELECT id, nombre, apellido FROM clintes";
                string libros = "SELECT id, nombre, autor, genero FROM libros";

                Console.WriteLine("Esta a punto de iniciar un préstamo, presionar ENTER para realizarlo");
                prestamo = Console.ReadLine();
                Console.Clear();
                tabla_usuarios();
                Console.WriteLine("Número de cliente: ");
                id_cliente = Console.ReadLine();
                Console.Clear();
                tabla_libros();
                Console.WriteLine("Código del libro: ");
                id_libro = Console.ReadLine();
                DateTime fechaInicio = DateTime.Now.Date;  // Tomamos solo la fecha (sin hora)

                // Fecha 7 días después
                DateTime fechaFin = fechaInicio.AddDays(7);

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO prestamo (fecha_prestamo, fecha_devolucion_estimada, id_libro, id_cliente) " + "VALUES (@fecha_prestamo, @fecha_devolucion_estimada, @id_libro, @id_cliente)";


                    try
                    {
                        using (MySqlCommand command = new MySqlCommand(query, con))
                        {
                            command.Parameters.AddWithValue("@fecha_prestamo", fechaInicio);
                            command.Parameters.AddWithValue("@fecha_devolucion_estimada", fechaFin);
                            command.Parameters.AddWithValue("@id_libro", id_libro);
                            command.Parameters.AddWithValue("@id_cliente", id_cliente);

                            int resultado = command.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                Console.WriteLine("\t\t\tPrestamo agendado correctamente!....");
                            }
                            else
                            {
                                Console.WriteLine("\t\t\tError al agendar el pretamo!...");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR al iniciar la conexión!!!");
                    }
                }

                    
                
            }

            void actualizar_prestamo()
            {
                string fecha_devolucion;
                string id;

                Console.WriteLine("Esta por modificar un prestamo, presione (ENTER) para continuar . . .");
                Console.ReadLine();
                Console.Clear();
                tabla_prestamo();
                Console.WriteLine("\nSeleccione el número de identificación del prestamo que desea cerrar: ");
                id = Console.ReadLine();
                Console.WriteLine("--> Fecha de devolucion (AÑO/MES/DIA): ");
                fecha_devolucion = Console.ReadLine();


                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "UPDATE prestamo SET fecha_devolucion_real=@fecha_devolucion_real WHERE id=@id";


                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@fecha_devolucion_real", fecha_devolucion);

                            int resultado = cmd.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                Console.WriteLine("\t\t\tDevolucion agendada correctamente!....");
                            }
                            else
                            {
                                Console.WriteLine("\t\t\tError al agendar la devolucion!...");
                            }
                        }
                        connection.Close();

                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }


            }


            // Crear la conexión
            using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    int opciones = -1;

                    while (opciones != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("------------MENU------------\n");
                        Console.WriteLine("1) -> Crear usuario");
                        Console.WriteLine("2) -> Actualizar usuario");
                        Console.WriteLine("3) -> Borrar usuario");
                        Console.WriteLine("4) -> Agregar libro");
                        Console.WriteLine("5) -> Actualizar libro");
                        Console.WriteLine("6) -> Borrar libro");
                        Console.WriteLine("7) -> Agregar géneros");
                        Console.WriteLine("8) -> Actualizar géneros");
                        Console.WriteLine("9) -> Crear prestamo");
                        Console.WriteLine("10) -> Actualizar préstamo");
                        Console.WriteLine("0) -> Salir\n");
                        Console.WriteLine("\rELIJA UNA DE LAS OPCIONES: ");
                        opciones = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();

                        switch (opciones)
                        {
                            case 0:
                                Environment.Exit(0);
                                break;
                            case 1:
                                crear_usuario();
                                break;
                            case 2:
                                actualizar_usuario();
                                break;
                            case 3:
                                borrar_usuario();
                                break;
                            case 4:
                                agregar_libro();
                                break;
                            case 5:
                                actualizar_libro();
                                break;
                            case 6:
                                borrar_libro();
                                break;
                            case 7:
                                agregar_genero();
                                break;
                            case 8:
                                actualizar_genero();
                                break;
                            case 9:
                                crear_prestamo();
                                break;
                            case 10:
                                actualizar_prestamo();
                                break;
                        }
                        Console.ReadLine();
                    }

                }
                Console.ReadLine();

            
        }
    }
}    

            
        

