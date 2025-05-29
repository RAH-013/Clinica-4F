DROP SCHEMA IF EXISTS clinica4f;
CREATE SCHEMA IF NOT EXISTS clinica4f;
USE clinica4f;

-- Tabla de Pacientes
CREATE TABLE  IF NOT EXISTS Pacientes (
    id_paciente CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    nombre_completo VARCHAR(100) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
    correo_electronico VARCHAR(100),
    telefono VARCHAR(20),
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Tabla de Médicos
CREATE TABLE  IF NOT EXISTS Medicos (
    id_medico CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    nombre_completo VARCHAR(100) NOT NULL,
    especialidad VARCHAR(100) NOT NULL,
    correo_electronico VARCHAR(100),
    telefono VARCHAR(20),
	fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Tabla de Citas
CREATE TABLE  IF NOT EXISTS Citas (
    id_cita CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    id_paciente CHAR(36) NOT NULL,
    id_medico CHAR(36) NOT NULL,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    estado ENUM('pendiente', 'confirmada', 'cancelada', 'completada') DEFAULT 'pendiente',
    FOREIGN KEY (id_paciente) REFERENCES Pacientes(id_paciente) ON DELETE RESTRICT,
    FOREIGN KEY (id_medico) REFERENCES Medicos(id_medico) ON DELETE RESTRICT
);

