﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace rDetalle.Entidades
{
    public class Personas
    {
        [Key]
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public DateTime FNacimiento { get; set; }

        [ForeignKey("PersonaId")]
        public virtual List<TelefonosDetalle> Telefonos { get; set; }

        public Personas()
        {
            PersonaId = 0;
            Nombre = string.Empty;
            Cedula = string.Empty;
            Direccion = string.Empty;
            FNacimiento = DateTime.Now;

            Telefonos = new List<TelefonosDetalle>();
        }
    }
}
