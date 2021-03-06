﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class Funcionario
    {

        //Atributos de la tabla Funcionario
            private String cedula;
            private String nombre;
            private String apellido1;
            private String apellido2;
            private String email;
            private String usuario;
            private String contrasena;
            private Boolean login;


            /* 
             * Encapsula los atributos de un usuario.
             * @param atributosUsuario Vector de tipo Object con los datos del usuario que se desea encapsular.
             */
            public Funcionario(Object[] datos)
            {
                this.cedula = datos[0].ToString();
                this.nombre = datos[1].ToString();
                this.apellido1 = datos[2].ToString();
                this.apellido2 = datos[3].ToString();
                this.email = datos[4].ToString();
                this.usuario = datos[5].ToString();
                this.contrasena = datos[6].ToString();
                this.login = Convert.ToBoolean(datos[7].ToString());
            }

            //Metodos set y get del atributo cedula
            public String getCedula
            {
                get { return cedula; }
                set { cedula = value; }
            }

            //Metodos set y get del atributo nombre
            public String getNombre
            {
                get { return nombre; }
                set { nombre = value; }
            }

            //Metodos set y get del atributo apellido1
            public String getApellido1
            {
                get { return apellido1; }
                set { apellido1 = value; }
            }

            //Metodos set y get del atributo apellido2
            public String getApellido2
            {
                get { return apellido2; }
                set { apellido2 = value; }
            }

            //Metodos set y get del atributo email
            public String getEmail
            {
                get { return email; }
                set { email = value; }
            }

            //Metodos set y get del atributo usuario
            public String getUsuario
            {
                get { return usuario; }
                set { usuario = value; }
            }

            //Metodos set y get del atributo contraseña
            public String getContrasena
            {
                get { return contrasena; }
                set { contrasena = value; }
            }

            //Metodos set y get del atributo login
            public Boolean getLogin
            {
                get { return login; }
                set { login = value; }
            }


        }
    }

