# Instructions to Run the Application

To run the application follows these steps:

First clone the repo 
  ```bash
   git clone https://github.com/JensIssa/PassswordManager
```
REMEMBER to install the MAUI package for .NET, as you wouldn't be able to run the application otherwise.

This can be done in the "Visual studio Installer". 

![image](https://github.com/user-attachments/assets/abd611c3-9568-4107-b708-c136024b90ad)

One would then be able to start up the application in visual studio. This application is currently only available in windows machine. Remember to choose a windows machine as the startup:

![image](https://github.com/user-attachments/assets/509d3fb9-9e95-473c-91bf-cb494f1bd7fa)



# Screenshots of the Product

Start up of the application, where you firstly set the masterpassword:

![image](https://github.com/user-attachments/assets/c758c169-5414-4b6d-813a-4fc652c2ffe3)

Then you can use that masterpassword to login to see the passwords. The masterpassword is saved in a hash in the bin folder.

When you login, you are able to see a list of the password:

![image](https://github.com/user-attachments/assets/cbcd3b32-419a-4f92-b344-4a272fb5a51d)

By pressing the icon on the right of the password, one is able to see the actual password:

![image](https://github.com/user-attachments/assets/c89ef7b4-f15a-45bc-ab6b-8a748c78d6bc)

You are able to create a new password with using the "Add new Password" button.

Then you see this page:

![image](https://github.com/user-attachments/assets/b3124d33-4ed2-4a7c-a3b8-51b5d5041836)


# Discussion About Security of Your Product

## What Do You Protect Against (Who Are the Threat Actors)

The actors I am protecting against could be the following:

People who are trying to gain unauthorized access to a device. This Password manager ensures, that even if someone accesses the device, they are not able to retrieve sensitive information like stored passwords, without them knowing the master password.

## What Is Your Security Model (Encryption, Key Handling, etc.)

The master password is never stored. Instead, it is hashed using Argon2id with the recommended configuration provided by OWASP: https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html. This ensures, that even if the hash file is comprised, it is expensive to derive the original master password.

Passwords stored in the database are encrypted, using a encryption algorithm (in my case, AES). The encryption is derived from the master password, using Argon2id, ensuring that without the master password, the stored passwords cannot be decrypted.

Also, the key for encryption is never stored directly. Instead, it is dynamically generated each time based on the master password entered by the user. This way, even if the database is comproised, the encryption key is never avaiable unless the master password is provided.

## Any Pitfalls or Limitations in Your Solution

The problem with the application is, that if the master password is lost or forgotten, there is no way to secure the encrypted passwords, because the encryption key is directly tied to the master password.

If the file storing the hash or the databse is comprised, threat actors may try to brute force it. This does lay on the user to make a strong master password.
