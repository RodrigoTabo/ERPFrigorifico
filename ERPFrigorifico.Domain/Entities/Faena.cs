using ERPFrigorifico.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class Faena
    {
        public int Id { get; set; }
        public DateTime FechaProduccion { get; set; }
        //*RECORDATORIO : Ajustar el proceso de Faena para que marque un tipo de proceso, y tambien ajustar el envio de animales (Si esta en curso, asigna automaticamente el animal,
        //sino, crea una nueva faena, es decir, una nueva produccion, esto es esencial para un nuevo dia) dependiendo el tipo de proceso de faena en curso (ESCENCIAL PARA UN FLUJO AUTOMATICO.)
        public TipoFaena TipoFaena { get; set; }
        public List<Canal> Canales { get; set; } = new List<Canal>();
        public List<Animal> Animales { get; set; } = new List<Animal>();
        public List<MovimientoAnimal> MovimientosAnimal { get; set; } = new List<MovimientoAnimal>();

    }
}
