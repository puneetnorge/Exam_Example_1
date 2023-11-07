using System;
using System.Collections.Generic;

namespace Exam_Example_1
{
    
    class Sensor
    {
        // Use property for Name
        public string Name { get; set; }

        // ReadData method
        public virtual void ReadData()
        {
            Console.WriteLine("Reading data from a generic sensor.");
        }

        // Write data method to write sensor data
        public virtual void WriteData(float val)
        {
            Console.WriteLine("Write data to a generic sensor");
        }
    }
    // Sensor class is the base class for TemperatureSensor
    class TemperatureSensor : Sensor
    {
        private float temp;
        //Overloaded contsructor to assign name
        public TemperatureSensor(string name)
        {
            Name = name;
        }
        public override void WriteData(float t1)
        {
            temp = t1;
        }
        public override void ReadData()
        {
            Console.WriteLine("Temperature is {0}", temp);
        }
    }

    class PressureSensor : Sensor
    {
        private float pressure;

        // overloaded contstructor to assign name
        public PressureSensor(string name)
        {
            Name = name;
        }

        public override void WriteData(float p1)
        {
            pressure = p1;
        }
        public override void ReadData()
        {
            Console.WriteLine("Pressure (in kPa) is {0}", pressure);
        }
    }

    class HumiditySensor : Sensor
    {
        private float humidity;
        public HumiditySensor(string name)
        {
            Name = name;
        }


        public override void WriteData(float h1)
        {
            humidity = h1;
        }
        public override void ReadData()
        {
            Console.WriteLine("Humidity is {0}", humidity);
        }
    }

    class CombinedSensor : Sensor
    {
        // In order to enable different combinations of Sensors List<T> can be used
        private List<Sensor> sen_list = new List<Sensor>();

        public CombinedSensor(string name)
        {
            Name = name;
        }

        /* Here the assumption is that a combined sensors is based a combination
         * of existing sessor objects that are already created */

        public void AddSensor(Sensor sen)
        {
            // check for null object
            if (sen == null)
            {
                Console.WriteLine("Object is null!");

            }
            else
            {
                // Add a selected Sensor to the List
                sen_list.Add(sen);
            }
        }

        public override void ReadData()
        {
            // Display the name of this combined sensor
            Console.WriteLine("Reading data from a combined sensor {0}", Name);

            foreach (var sen in sen_list)
            {
                // To avoid null exception
                if (sen != null)
                {
                    sen.ReadData();
                }

            }
        }

        public override void WriteData(float val)
        {
            Console.WriteLine("Cannot write data to a combined sensor.");
        }
    }

    class Program
    {
        static void Main()
        {
            Sensor temp_ref = new TemperatureSensor("Temperature Sensor");
            Sensor pressure_ref = new PressureSensor("Pressure Sensor");
            Sensor humidity_ref = new HumiditySensor("Humidity Sensor");
            temp_ref.WriteData(25.5F);
            pressure_ref.WriteData(1013.2F);
            humidity_ref.WriteData(50.0F);

            Console.WriteLine("Three objects and their values");
            // Display the data from the three special reference objects
            temp_ref.ReadData();
            pressure_ref.ReadData();
            humidity_ref.ReadData();

            // Combining Temp and Pressure
            CombinedSensor combined_TP = new CombinedSensor("Combined Temp & Pressure Sensor");

            combined_TP.AddSensor(temp_ref);
            combined_TP.AddSensor(pressure_ref);

            combined_TP.ReadData();

            // Combining Temp, Pressure, and Humidity
            CombinedSensor combined_TPH = new CombinedSensor("Combined Temp, Pressure, and Humidity Sensor");

            combined_TPH.AddSensor(temp_ref);
            combined_TPH.AddSensor(pressure_ref);
            combined_TPH.AddSensor(humidity_ref);

            combined_TPH.ReadData();
            Console.ReadKey();
        }
    }

    /* Concepts used for the above code:
     * 
     * I used the concept of Inheritance and in particular dynamic polymoriphism learned 
     * in the lectures. ReadData and WriteData are virtual methods in the base class called Sensor
     * 
     * All other sensor classes such as Temp.Sensor, PressureSensor, and HumiditySensor,
     * use Sensor class as base class.
     * 
     * TemperatureSensor has a private member called temp, similarly other derived classes 
     * have a private member each.
     * I use overloaded contructors to assign the name
     * 
     * Some Limitations of the above code:
     * 1) We can still define an object of Sensor (base) class which is not useful at all
     * A remedy would be to use Abstract classes
     * 
     * 2) Combined Sensor cannot be given new values so we cannot assign new values of 
     * temperature, pressure, or humidity.
     * 
     * 3) I am not sure if having a private member in the derived classes is a good practice
     *
     * 
     * */
}
