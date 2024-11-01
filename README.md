Base de datos "bibliotp" en MySql
Estos son los datos de las tablas creadas en la base de datos para que el programa funcione correctamente:

CREATE TABLE clientes(
	 id INT NOT NULL AUTO_INCREMENT,
	 nombre VARCHAR(30) NOT NULL,
	 apellido VARCHAR(30) NOT NULL,
	 dni VARCHAR(8) NOT NULL,
	 telefono VARCHAR(40) NOT NULL,
	 email VARCHAR(40) NOT NULL,
	 creado_el TIMESTAMP DEFAULT NOW(),
	 actualizado_el TIMESTAMP NOW(),
	 estado TINYINT(3) NULL DEFAULT(1),
	PRIMARY KEY (id));

 CREATE TABLE genero(
	 id INT NOT NULL AUTO_INCREMENT,
	 genero VARCHAR(20) NOT NULl,
	PRIMARY KEY (id));

 CREATE TABLE libros(
	 id INT NOT NULL AUTO_INCREMENT,
	 nombre VARCHAR(40) NOT NULL,
	 autor VARCHAR(40) NOT NULL,
	 fecha_lanzamiento DATE,
	 id_genero INT,
   creado_el TIMESTAMP DEFAULT NOW(),
	 actualizado_el TIMESTAMP NOW(),
	 estado TINYINT DEFAULT(1),
	PRIMARY KEY (id));

 CREATE TABLE prestamo(
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
