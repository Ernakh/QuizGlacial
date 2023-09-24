using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class Criptografia
{
    //public void EncryptAesManaged(string raw)
    //{
    //    try
    //    {
    //        // Create Aes that generates a new key and initialization vector (IV).    
    //        // Same key must be used in encryption and decryption    
    //        using (AesManaged aes = new AesManaged())
    //        {
    //            // Encrypt string    
    //            byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
    //            // Print encrypted string    
    //            Console.WriteLine($"Dado criptografado: {System.Text.Encoding.UTF8.GetString(encrypted)}");
    //            // Decrypt the bytes to a string.    
    //            string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
    //            // Print decrypted string. It should be same as raw data    
    //            Console.WriteLine($"Dado descriptografado: {decrypted}");
    //        }
    //    }
    //    catch (Exception exp)
    //    {
    //        Console.WriteLine(exp.Message);
    //    }
    //}
    //private byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
    //{
    //    byte[] encrypted;
    //    // Create a new AesManaged.    
    //    using (AesManaged aes = new AesManaged())
    //    {
    //        // Create encryptor    
    //        ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
    //        // Create MemoryStream    
    //        using (MemoryStream ms = new MemoryStream())
    //        {
    //            // Create crypto stream using the CryptoStream class. This class is the key to encryption    
    //            // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
    //            // to encrypt    
    //            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
    //            {
    //                // Create StreamWriter and write data to a stream    
    //                using (StreamWriter sw = new StreamWriter(cs))
    //                {
    //                    sw.Write(plainText);
    //                }

    //                encrypted = ms.ToArray();
    //            }
    //        }
    //    }
    //    // Return encrypted data    
    //    return encrypted;
    //}
    //private string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
    //{
    //    string plaintext = null;
    //    // Create AesManaged    
    //    using (AesManaged aes = new AesManaged())
    //    {
    //        // Create a decryptor    
    //        ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
    //        // Create the streams used for decryption.    
    //        using (MemoryStream ms = new MemoryStream(cipherText))
    //        {
    //            // Create crypto stream    
    //            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
    //            {
    //                // Read crypto stream    
    //                using (StreamReader reader = new StreamReader(cs))
    //                    plaintext = reader.ReadToEnd();
    //            }
    //        }
    //    }
    //    return plaintext;
    //}

    public string EncryptData(string textData, string Encryptionkey)
    {
        RijndaelManaged objrij = new RijndaelManaged();
        //iniciar o modo de operação
        objrij.Mode = CipherMode.CBC;
        //setar o padding de operação da string
        objrij.Padding = PaddingMode.PKCS7;
        //setar o tamanho (em bits) da operação 
        objrij.KeySize = 0x80;
        //setar o tamanho do bloco    
        objrij.BlockSize = 0x80;
        //setar a chave de criptografia
        byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
        //setar o vetor de bytes de inicialização para criptografia
        byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        int len = passBytes.Length;
        if (len > EncryptionkeyBytes.Length)
        {
            len = EncryptionkeyBytes.Length;
        }
        Array.Copy(passBytes, EncryptionkeyBytes, len);

        objrij.Key = EncryptionkeyBytes;
        objrij.IV = EncryptionkeyBytes;

        //Cria uma chave simétrica
        ICryptoTransform objtransform = objrij.CreateEncryptor();
        byte[] textDataByte = Encoding.UTF8.GetBytes(textData);
        //Converte para string e retorna
        return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
    }

    public string DecryptData(string EncryptedText, string Encryptionkey)
    {
        RijndaelManaged objrij = new RijndaelManaged();
        objrij.Mode = CipherMode.CBC;
        objrij.Padding = PaddingMode.PKCS7;

        objrij.KeySize = 0x80;
        objrij.BlockSize = 0x80;
        byte[] encryptedTextByte = Convert.FromBase64String(EncryptedText);
        byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
        byte[] EncryptionkeyBytes = new byte[0x10];
        int len = passBytes.Length;
        if (len > EncryptionkeyBytes.Length)
        {
            len = EncryptionkeyBytes.Length;
        }
        Array.Copy(passBytes, EncryptionkeyBytes, len);
        objrij.Key = EncryptionkeyBytes;
        objrij.IV = EncryptionkeyBytes;
        byte[] TextByte = objrij.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
        return Encoding.UTF8.GetString(TextByte);  //irá retornar a string original descriptografada
    }
}
