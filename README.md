Given the size and nature of the project we will have a single branch from which we work called master.  

Before you begin, clone our github.com repository.  This only needs to be done once but can be done as many time as you'd like.  But be cautious about cloning into an already existing git repository.  You will do _all_ of your work within the folder created by git clone.

git clone https://github.com/ashawnbandy/cecs491

Generally, you will want to go through the following cycle for work corresponding to a couple of hours of work - generally a single additional feature or bug fix.

--== Session Workflow ==--

0. Begin by updating your master branch to catch up with any changes that have been made by other team members since your last session:

git pull origin master

1. Create a branch from which to work.

git checkout -b <your initials>_<date>_<very short description>

example: git checkout -b asb_20120912_addedclownclass

2. Do your work.  

3. You _may_ wish during your work to keep up with any changes that have been made since you began work on your branch:

git commit -m "<message>"

git checkout master

git pull

git checkout <branch you are working on>

git rebase master 


4. When your work is complete, add your files, commit and push to the github repository:

git add .
git commit
git push origin master

Note that git commit will launch a text editor.  Be as detailed as you can about what was done and uncomment the files that were added and removed (automatically added to the text file by git).

5. Send an email to the group to let us know that you've added a branch.  I will then merge your branch into the master.

6. Repeat.
