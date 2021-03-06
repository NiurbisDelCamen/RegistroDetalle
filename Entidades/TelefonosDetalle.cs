﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace rDetalle.Entidades
{
    public class TelefonosDetalle
    {
        [Key]
        public int Id { get; set; }
        public string TipoTelefono { get; set; }
        public string Telefono { get; set; }
        public int PersonaId { get; set; }

        public TelefonosDetalle()
        {
            Id = 0;
            PersonaId = 0;
            TipoTelefono = string.Empty;
            Telefono = string.Empty;

        }
    }
}
