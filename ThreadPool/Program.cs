//// encrypt in another thread;
//// after that
//Console.WriteLine("File Encrypted Successfuly");


using System;
using System.Text;/////////console baslayanda readkey var!
void Encryption(string path, ConsoleKeyInfo key)
{
    if (File.Exists(path) && Path.GetExtension(path) == ".txt")
    {
        var str = new StringBuilder();
        var text = File.ReadAllText(path);
        char keyChar = key.KeyChar;
        foreach (var item in text)
        {
            str.Append(item ^ keyChar);
        }
        var encryption = str;
        var newFileName = $"{Path.GetFullPath(path).Replace(".txt", "")}Encrypted.txt";//burada sonuna extentiouda artirirdi ona gore sildim onsuzsa sonda txt yaz yeni duz isleyir kod(ingilis keyboard)
        //if (!File.Exists(newFileName))
        //{
        //    File.Create(newFileName) ;
        //}
        using (var writer = new StreamWriter(newFileName))
        {
            writer.WriteLine(str.ToString());
        }
        Console.WriteLine($"\n{encryption} \nHas Been Wrote Here : {Path.GetFullPath(newFileName)}\nClick Enter");
        Console.ReadKey();
    }
    else { Console.Clear(); Console.WriteLine("False File Extension Or False Path\nPress Any Key For Continue."); Console.ReadKey(); }
}

while (true)
{
here:
    Console.ReadKey();
    Console.Clear();
    Console.Write("Enter Path (.txt) : ");
    var path = Console.ReadLine();
    Console.Write("Enter Key : ");
    var key = Console.ReadKey();

    if (path is not null)
    {
        try
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    Encryption(path, key);
                    Console.WriteLine("File Encrypted Successfuly"); //
                }
                catch { Console.WriteLine("\nHave Problem !"); }; // problem olsa thread ile yoxlaya bilersiz!
            });


            //Thread thread = new(() => Encryption(path,key));
            //thread.Start();

        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); goto here; }
    }
    else { Console.WriteLine("Can't Be Null Path\nPress Any Key"); Console.ReadKey(); }
}