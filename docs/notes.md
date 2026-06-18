# Notes

git switch -c dev
-c means create a new branch and switch to it immediately.

git log --oneline --decorate --graph --all
--oneline: shows each commit in one compact line.
--decorate: shows branch labels like HEAD -> dev, main.
--graph: draws the commit history as an ASCII graph.
--all: shows all branches, not only the current one.


Get-Content contracts\json\lesson-01-ticket.json | ConvertFrom-Json

Get-Content: reads a file.
|: sends the output of the left command into the right command. This is called a pipe.
ConvertFrom-Json: asks PowerShell to parse the text as JSON.
If the JSON is valid, PowerShell will print the object nicely. If it is invalid, it will show an error.