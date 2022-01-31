# ProgSysteme

This project is a C#/.NET Core application developed by BRIAND Lucile, DIAS Bruno, KHADHAR Dany and MONAQUE Victor.

# User guide
- [French](https://github.com/Lucile-BRIAND/ProgSysteme/tree/AppV3#french) 
- [English](https://github.com/Lucile-BRIAND/ProgSysteme/tree/AppV3#english) 

## French
### 1.	Démarrage de l’application
Le démarrage de l’application se fait à l’aide d’un fichier exécutable. En lançant l’application, une console s’ouvre, et on accède au menu principal.

### 2.	Choix de la langue
Lors du lancement de l’application, le choix de la langue est automatiquement demandé. Pour le moment, seuls le français et l’anglais sont disponibles, mais il est à tout moment possible d’en ajouter.

Pour cela, nous avons conçu notre application de manière à ce que les langues soient gérées dans des fichiers précis. Chaque fichier JSON contient les données nécessaires à l’affichage des menus pour un langage. Il est donc tout à fait possible d’en ajouter et de traduire les données, sans avoir besoin de modifier le code source.

### 3.	Création d’un travail de sauvegarde
La première option du menu principal est la création d’un travail de sauvegarde. L’application nous permet d’en créer plusieurs et de stocker ces travaux dans un fichier JSON. Ainsi, ces travaux sont gardés en mémoire au lieu d’être perdus entre chaque démarrage d’EasySave.

Le logiciel demande d’entrer dans l’ordre :
-	Un nom de sauvegarde
-	Un type de sauvegarde : différentiel ou complet
-	L’emplacement source des fichiers à sauvegarder
-	L’emplacement de destination souhaité 

Une fois la création effectuée, l’utilisateur est automatiquement renvoyé vers le menu principal. Si une information n’est pas correctement entrée, il doit recommencer sa saisie jusqu’à ce qu’elle soit valide.

### 4.	Exécution d’un travail de sauvegarde
La deuxième option du menu principal est l’exécution d’une ou plusieurs sauvegardes. La console affiche la liste des sauvegardes enregistrées ainsi que leurs caractéristiques, et demande si l’utilisateur veut choisir une sauvegarde à exécuter, ou bien exécuter toutes les sauvegardes enregistrées. 

Si l’utilisateur choisit de n’en exécuter qu’une, il doit alors entrer le nom de la sauvegarde voulue. Une fois l’exécution effectuée, il est automatiquement renvoyé vers le menu principal. Si une information n’est pas correctement entrée, il doit recommencer sa saisie jusqu’à ce qu’elle soit valide.

### 5.	Suppression d’un travail de sauvegarde
Enfin, la troisième et dernière option du menu principal est la suppression d’une sauvegarde enregistrée. La console affiche la liste des sauvegardes enregistrées ainsi que leurs caractéristiques, et demande à l’utilisateur d’entrer le nom de la sauvegarde qu’il veut supprimer.  

Une fois la suppression effectuée, il est automatiquement renvoyé vers le menu principal. Si une information n’est pas correctement entrée, il doit recommencer sa saisie jusqu’à ce qu’elle soit valide.

### 6.	Journaux d’activité
Les journaux d’activités sont créés pour permettre à l’utilisateur de vérifier à tout moment le bon déroulement de ses sauvegardes. Il existe deux types de journaux : le premier répertorie toutes les sauvegardes correctement exécutées durant la journée, et le second affiche en temps réel l’avancement des sauvegardes. 
Une fois créés, ces fichiers sont disponibles dans le dossier « bin/Debug/netcoreapp3.1/ ».
 
 
 
## English
### 1.	Starting the application
The application is started using an executable file. When launching the application, a console opens, and the main menu is accessed.

### 2.	Choosing the language
When the application is launched, the choice of language is automatically requested. For the moment, only French and English are available, but it’s possible to add more languages at any time.

In order to do this, we have designed our application so that the languages are managed in specific files. Each JSON file contains the data necessary to display the menus for a language. It is therefore entirely possible to add and translate the data, without having to modify the source code.

### 3.	Creating a job
The first option in the main menu is the creation of a backup job. The application allows us to create several jobs and store them in a JSON file. This way, these jobs are kept in memory instead of being lost between each start of EasySave.

The software asks to enter in order : 
-	A backup name
-	A backup type: differential or full
-	The source location of the files to backup
-	The desired destination location

Once the creation is complete, the user is automatically returned to the main menu. If a piece of information is not correctly entered, the user must repeat the entry until it is valid.

### 4.	Running a job
The second option in the main menu is to run one or more backups. The console displays a list of saved backups and their characteristics, and asks if the user wants to choose one backup to run, or run all saved backups.

If the user chooses to run only one backup, they must enter the name of the backup they want to run. Once executed, the user is automatically returned to the main menu. If any information is entered incorrectly, the user must re-enter it until it is valid.

### 5.	Deleting a job
Finally, the third and last option in the main menu is to delete a saved backup. The console displays a list of saved backups and their characteristics, and asks the user to enter the name of the backup they want to delete. 

Once deleted, the user is automatically returned to the main menu. If a piece of information is not correctly entered, the user must repeat the entry until it is valid.

### 6.	Log files
Activity logs are created to allow the user to check at any time that their backups are running correctly. There are two types of logs: the first lists all the backups that were successfully executed during the day, and the second displays the progress of the backups in real time.

Once created, these files are available in the « bin/Debug/netcoreapp3.1/ » folder.
 
