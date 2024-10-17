using System;

interface IPower
{
    void Digunakan(Robot target);
}

abstract class Robot
{
    public string nama { get; set; }
    public int energi { get; set; }
    public int armor { get; set; }
    public int serangan { get; set; }

    public Robot (string nama, int energi, int armor, int serangan)
    {
        this.nama = nama;
        this.energi = energi;
        this.armor = armor;
        this.serangan = serangan;
    }

    public abstract void Serang (Robot target, IPower serangan);

    public void InfoRobot()
    {
        Console.WriteLine ($"Nama robot : {nama} \nEnergi : {energi} \nArmor : {armor} \nSerangan : {serangan}");
    }
}

class BosRobot : Robot
{
    public int pertahanan { get; set; }
    public BosRobot(string nama, int energi, int armor, int serangan, int pertahanan) : base(nama, energi, armor, serangan)
    {
        this.pertahanan = pertahanan;
    }
    public override void Serang(Robot target, IPower serangan)
    {
        Console.WriteLine($"Robot {nama} menyerang Robot {target.nama}");
        serangan.Digunakan(target);  
        Console.WriteLine($"NOTE : Energi {target.nama} setelah diserang: {target.energi}");
    }

    public void Diserang(Robot penyerang)
    {
        Console.WriteLine($"{nama} diserang oleh Robot {penyerang.nama}");
        energi -= (penyerang.serangan - pertahanan);
        if (energi < 0)
        {
            energi = 0;
        }

        Console.WriteLine($"NOTE : Energi {nama} setelah diserang: {energi}");

        if (energi <= 0)
        {
            Mati();  
        }
    }
    public void Mati()
    {
        Console.WriteLine($"Astaga! Robot {nama} telah K.O, Gambatte!");
    }
}

class Robotbiasa : Robot
{
    public Robotbiasa (string nama, int energi, int armor, int serangan) : base(nama, energi, armor, serangan)
    {
       
    }

    public override void Serang(Robot target, IPower serangan)
    {
        Console.WriteLine($"Robot {nama} menyerang Robot {target.nama}");
        serangan.Digunakan(target);
        Console.WriteLine($"NOTE : Energi {target.nama} setelah diserang: {target.energi}");
    }

    public void Diserang(Robot penyerang)
    {
        Console.WriteLine($"{nama} diserang oleh Robot {penyerang.nama}");
        energi -= (penyerang.serangan);
        if (energi < 0)
        {
            energi = 0;
        }

        Console.WriteLine($"NOTE : Energi {nama} setelah diserang: {energi}");

        if (energi <= 0)
        {
            Mati();
        }
    }
    public void Mati()
    {
        Console.WriteLine($"Astaga! Robot {nama} telah K.O, Gambatte!");
    }
}
class  Perbaikan : IPower
{
    public void Digunakan(Robot target)
    {
        target.energi += 20;
        Console.WriteLine($"Robot {target.nama} melakukan perbaikan sebanyak 20 heal");
    }
}
class SeranganListrik : IPower
{
    public void Digunakan(Robot target)
    {
        target.energi -= 25; 
        Console.WriteLine($"{target.nama} terkena Serangan Listrik! Energi berkurang 25.");
    }
}

class SeranganPlasma : IPower
{
    public void Digunakan(Robot target)
    {
        target.energi -= 35;
        Console.WriteLine($"{target.nama} terkena Serangan Plasma! Energi berkurang 35.");
    }
}

class Program
{
     static void Main (string[] args)
    {
        BosRobot robot1 = new BosRobot("Putra", 100,20,30,15);
        Robotbiasa robot2 = new Robotbiasa("Cece", 100, 10, 20);
        Perbaikan recall = new Perbaikan(); 
        Console.WriteLine("====================================");

        robot1.InfoRobot();
        Console.WriteLine();
        robot2.InfoRobot();
        Console.WriteLine("====================================");

        IPower SeranganListrik = new SeranganListrik();
        robot1.Serang(robot2, SeranganListrik);
        Console.WriteLine() ;
        IPower SeranganPlasma = new SeranganPlasma();
        robot2.Serang(robot1, SeranganPlasma);
        recall.Digunakan(robot1);
    }
}

