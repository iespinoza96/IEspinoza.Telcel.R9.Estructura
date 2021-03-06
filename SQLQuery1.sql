USE [IEspinozaTelcelR9Estructura]
GO
/****** Object:  StoredProcedure [dbo].[EmpleadoGetAll]    Script Date: 4/28/2022 11:47:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--STORED PROCEDURE 
--Permitir consultar todos los empleados son sus puestos y departamentos 

ALTER PROCEDURE [dbo].[EmpleadoGetAll]
AS 
	SELECT 
		 Empleado.EmpleadoID
		,Empleado.Nombre AS Nombre
		,Puesto.PuestoID
		,Puesto.Descripcion AS NombrePuesto 
		,Departamento.DepartamentoID
		,Departamento.Descripcion AS NombreDepartamento

		FROM Empleado

			INNER JOIN Puesto 
				ON Empleado.PuestoID = Puesto.PuestoID

				INNER JOIN Departamento
					ON Empleado.DepartamentoID = Departamento.DepartamentoID
