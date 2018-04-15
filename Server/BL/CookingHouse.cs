using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Models;

namespace Server.BL
{

   class Task
    {
        protected Executor _Executor { get; set; }
        protected Item _Item { get; set; }
        public Task(Executor e, Item i)
        {
            this._Executor = e;
            this._Item = i;
        }

        public void Execute()
        {
            if (_Executor != null)
                _Executor.Cook(_Item);
            else
                return;
        }
    } 
   public abstract class Executor
    {
        public abstract void Cook(Item item);
    }

    public class SoupDepartment : Executor
    {
        public override void Cook(Item item)
        {
            item.Status = true;
        }
    }

    public class DessertDepartment : Executor
    {
        public override void Cook(Item item)
        {
            item.Status = true;
        }
    }

    public class AppetizerDepartment: Executor
    {
        public override void Cook(Item item)
        {
            item.Status = true;
        }
    }
}