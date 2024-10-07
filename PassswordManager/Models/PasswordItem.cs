

public class PasswordItem
{
    public string Name { get; set; }
    public string EncryptedPassword { get; set; }

    public string GetDecryptedPassword(string masterPassword)
    {
        return EncryptionService.Decrypt(EncryptedPassword, masterPassword);
    }

    public void SetPassword(string plainPassword, string masterPassword)
    {
        EncryptedPassword = EncryptionService.Encrypt(plainPassword, masterPassword);
    }
}
