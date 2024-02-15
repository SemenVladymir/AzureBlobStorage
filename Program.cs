using AzureBlobStorage;

AzureBlobClient.GetInstance();
string inp;
do
{
    Console.Clear();
    Console.WriteLine("What do you want to do?\n\n");
    Console.WriteLine("1. Read all files in the Azure blob\n");
    Console.WriteLine("2. Add new file to the Azure blob\n");
    Console.WriteLine("3. Upload file into the Azure blob\n");
    Console.WriteLine("4. Delete file from the Azure blob\n\n\n");
    Console.WriteLine("0. Exit from program\n");
    inp = Console.ReadLine();
    if (int.TryParse(inp, out int res))
    {
        switch (res)
        {
            case 1:
                await AzureBlobClient.ShowFilesList();
                break;
            case 2:
                await AzureBlobClient.AddBlobFile();
                break; 
            case 3:
                await AzureBlobClient.UploadBlob();
                break;
            case 4:
                await AzureBlobClient.DeleteBlob();
                break;
            case 0:
                return;
        }
    }
} while (true);