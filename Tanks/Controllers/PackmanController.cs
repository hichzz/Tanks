using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.Models;

namespace Tanks.Controllers
{
    public class PackmanController
    {
         private FieldController fieldController;
        public PackmanController(string[] args)
        {
            fieldController = new FieldController(args);
        }

        public Field GetField() => fieldController.Field;
    }
}
