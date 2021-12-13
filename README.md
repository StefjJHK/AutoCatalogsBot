# AutoCatalogsBot
Bot for creating catalogs automaticly in group https://vk.com/warattekudasai.

When new post are published in group, bot automaticly scans it and update catalogs if post suit by some format.
To determine if a post is suitable, using RegularExpressions.
If post sits partially, bot send warning notification to admin discussions, or nothing if post doesn't suit at all.
Users can send to bot 2 commands: "бот обновить каталоги" and "бот каталоги", to refresh all catalogs in group and get all catalogs from google sheets.

Catalogs are stored in GoogleSheets.
ConnectionString and other serets are stored in Azure KeyVault.
