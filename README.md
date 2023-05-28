# TechedMVC

## Kod
<br />

Kako se zna dogoditi da se dobije greška "429 TooManyRequest" pozivajući API često, neke metode unutar Home i Coin kontrolera sam su napravljene na dva načina. 
<br />

![](Readme_Images/Home_Details_Code.png "Home Details Code")

Obje metode, i Details i Details2 metode prikazuju odgovarajuće podatke na pripradajućim View stranicama. Jedna prima ID te nanovo popuni listu pozivajuci API.
Druga prima cijeli objekt u JSON formatu te ga takvog pretvara u odgovarajući objekt.
<br />
<br />
<br />

![](Readme_Images/Coin_Add_Code.png "Coin Add Code")

Isto kao u primjeru iznad, jedna metoda prima Id, poziva API kako bi popunila listu te dohvati odgovarajući objekt iz liste. Druga prima cijeli objekt u JSON formatu.
<br />
<br />
<br />

## Slike projekta
<br />

![](Readme_Images/Home_Index.png "Home Index page")
*/Home page*
<br />
<br />

![](Readme_Images/Home_Details.png "Home Details page")
*/Home/Details/ page*
<br />
<br />

![](Readme_Images/Coin_Home.png "Coin Index page")
*/Coin page*
<br />
<br />

![](Readme_Images/Coin_Details.png "Coin Details page")
*/Coin/Details/ page*
<br />
<br />

![](Readme_Images/Coin_Edit.png "Coin Edit page")
*/Coin/Edit/ page*
<br />
<br />

![](Readme_Images/Coin_Edit_Photo.png "Edit Photo preview")
<br />
<br />

![](Readme_Images/Coin_Delete.png "Delete Coin page")
*/Coin/Delete/ page*

<br />
<br />
<br />

## Baza podataka
<br />

Za izradu baze podataka korišten je Entity Framework sa Code-First pristupom. "Coin" tablica je izgenerirana po CoinEntity model klasi.

<br />

![](Readme_Images/Database_Select.png "Database SELECT")
*Pregled spremljenih podataka u bazi podataka*
<br />
<br />

![](Readme_Images/Database_Create.png "Database CREATE")
*Generirana skripta za izradu tablice*