# NTP

## A. Récupération de l'heure actuelle à partir d'un serveur NTP

Objectif : Écrire un programme qui récupère l'heure actuelle à partir d'un serveur NTP et l'affiche à l'écran.

Instructions :

1. Créez un nouveau projet console en C# dans Visual Studio.
2. Ajoutez la bibliothèque `System.Net.Sockets` à votre projet.
3. Dans le fichier `Program.cs`, importez les espaces de noms suivants :

```csharp
using System;
using System.Net;
using System.Net.Sockets;
```

4. Dans la méthode `Main()`, créez une variable `ntpServer` qui contient l'adresse IP ou le nom de domaine d'un serveur
   NTP public, tel que `0.ch.pool.ntp.org`.

```csharp
string ntpServer = "0.ch.pool.ntp.org";
```

5. Créez une variable `timeMessage` de type `byte[]` avec une taille de 48 octets et l'initialiser ainsi :

```csharp
byte[] timeMessage = new byte[48];
ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)
```

6. Créez une variable `ntpReference` de type `IPEndPoint` en utilisant le port 123 (port
   standard du protocole NTP).

```csharp
IPEndPoint ntpReference = new IPEndPoint(Dns.GetHostAddresses(ntpServer)[0], 123);
```

7. Créez une variable `client` de type `UdpClient` et connectez-la au serveur NTP.

```csharp
UdpClient client = new UdpClient();
ntpClient.Connect(client);
```

8. Envoyez une demande NTP au serveur en utilisant la méthode `Send()` de la classe `UdpClient`.

```csharp
client.Send(timeMessage, timeMessage.Length);
```

9. Recevez la réponse NTP du serveur en utilisant la méthode `Receive()` de la classe `UdpClient`.

```csharp
timeMessage = client.Receive(ref ntpReference);
```

10. Convertissez les données NTP reçues en un objet `DateTime` en utilisant la méthode `ToDateTime()` de la
    classe `NtpPacket`.

```csharp
DateTime ntpTime = NtpPacket.ToDateTime(ntpData);
```

<details><summary>NtpPacket ??</summary>

Voici de quoi remplir NtpPacket
```csharp
ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);
```

</details>

11. Affichez l'heure actuelle à l'écran en utilisant la méthode `ToString()` de la classe `DateTime`.

```csharp
Console.WriteLine($"Heure actuelle : {ntpTime}");
```

12. Fermez la connexion au serveur NTP en utilisant la méthode `Close()` de la classe `UdpClient`.

```csharp
client.Close();
```

13. Exécutez le programme et vérifiez que l'heure actuelle est affichée correctement à l'écran.

## B. Formats de date

### 1. Afficher l'heure actuelle dans différents formats de date :
    
    - lundi, 16 août 2021
    - 25.10.2024 08:00:00
    - 10.10.2024

<details><summary>Aide</summary>

```csharp
Console.WriteLine($"Heure actuelle (format personnalisé) : {ntpTime.ToString("dd/MM/yyyy HH:mm:ss")}");
```
</details>

## C. Conversions temporelles

### 1. Calculer la différence de temps entre l'heure locale et l'heure NTP :

<details><summary>Voir la réponse</summary>

```csharp
TimeSpan timeDiff = DateTime.Now - ntpTime;
Console.WriteLine($"Différence de temps : {timeDiff.TotalSeconds} secondes");
```
</details>


### 2. Corriger l'heure actuelle avec l'heure NTP et afficher le tout en heure locale :

<details><summary>Voir la réponse</summary>

```csharp
DateTime localTime = ntpTime.Add(timeDiff);
Console.WriteLine($"Heure locale : {localTime}");
```
</details>


### 3. Convertir l'heure locale en heure UTC :

<details><summary>Voir la réponse</summary>

```csharp
DateTime utcTime = localTime.ToUniversalTime();
Console.WriteLine($"Heure UTC : {utcTime}");
```
</details>


### 4. Convertir l'heure UTC en heure locale :


<details><summary>Voir la réponse</summary>

```csharp
localTime = utcTime.ToLocalTime();
Console.WriteLine($"Heure locale (à partir de l'heure UTC) : {localTime}");
```
</details>


### 5. Convertir l'heure locale en heure GMT :

<details><summary>Voir la réponse</summary>

```csharp
DateTime gmtTime = localTime.ToUniversalTime().AddHours(-1); // Suisse en GMT+1
Console.WriteLine($"Heure GMT : {gmtTime}");
```
</details>


### 6. Convertir l'heure GMT en heure locale :


<details><summary>Voir la réponse</summary>

```csharp
localTime = gmtTime.AddHours(1); // Ajustez l'ajustement horaire en fonction de votre fuseau horaire
Console.WriteLine($"Heure locale (à partir de l'heure GMT) : {localTime}");
```
</details>

