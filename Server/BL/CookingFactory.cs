using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.BL
{
    public abstract class CookingFactory
    {
        public abstract DessertCourse CreateDessertCourse();
        public abstract MainCourse CreateMainCourse();
        public abstract AppetizerCourse CreateAppetizerCourse();
    }

    public class ChineeseFactory : CookingFactory

    {
        public override AppetizerCourse CreateAppetizerCourse()
        {
            return new ChineeseAppetizerCourse();
        }

        public override DessertCourse CreateDessertCourse()
        {
            return new ChineeseDessertCourse();
        }

        public override MainCourse CreateMainCourse()
        {
            return new ChineeseMainCourse();
        }
    }

    public class ItalianFactory : CookingFactory
    {
        public override AppetizerCourse CreateAppetizerCourse()
        {
            return new ItalianAppetizerCourse();
        }

        public override DessertCourse CreateDessertCourse()
        {
            return new ItalianDessertCourse();
        }

        public override MainCourse CreateMainCourse()
        {
            return new ItalianMainCourse();
        }
    }


    public abstract class MainCourse { }
    public abstract class DessertCourse { }
    public abstract class AppetizerCourse { }

    public class ChineeseMainCourse : MainCourse { }
    public class ChineeseDessertCourse: DessertCourse { }
    public class ChineeseAppetizerCourse : AppetizerCourse { }

    public class ItalianMainCourse : MainCourse { }
    public class ItalianDessertCourse : DessertCourse { }
    public class ItalianAppetizerCourse : AppetizerCourse { }


}