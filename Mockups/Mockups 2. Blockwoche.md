
**Hauptmenü (Header)**

```markdown
+------------------------------------------------------------------+
|                     Bibliotheksverwaltung                        |
+------------------------------------------------------------------+
|  [Bücherverwaltung]  [Schülerverwaltung]  [Ausleihe / Rückgabe]  |
|  [Statistik]  [Ausgeliehene Bücher]  [Exportieren]  [Importieren] |
+------------------------------------------------------------------+
```

---

**Bücherverwaltung**

```markdown
+--------------------------------------+
|           Bücherverwaltung           |
+--------------------------------------+
| [Einfügen]  [Suchen]                 |
+--------------------------------------+
| Suchfeld: [______________________]    |
+--------------------------------------+
| Bücherliste:                          |
|  Buchnummer    | Titel               | Autor         | Verlag         | ISBN         | Verlagsort | Erscheinungsdatum |           |
| -------------- | ------------------- | ------------- | -------------- | ------------ | ---------- | ----------------- |-----------|
| 00001-2023     | Einführung in C#    | Max Muster    | Tech Verlag    | 978-3-16-148 | Wien       | 2023              | [Bearbeiten] [Löschen] |
| 00002-2022     | Grundlagen Java     | Erika Muster  | Code Verlag    | 978-1-23-456 | Berlin     | 2022              | [Bearbeiten] [Löschen] |
+--------------------------------------+
| Klicke auf "Bearbeiten" für Änderungen oder "Löschen" zum Entfernen |
+--------------------------------------+
```

**Einfügeformular für Bücher**

```markdown
+--------------------------------------+
|           Buch hinzufügen            |
+--------------------------------------+
| Buchnummer:       [__________]       |
| Titel:            [__________]       |
| Autor:            [__________]       |
| Verlag:           [__________]       |
| ISBN:             [__________]       |
| Verlagsort:       [__________]       |
| Erscheinungsdatum:[__________]       |
+--------------------------------------+
|             [Speichern]              |
+--------------------------------------+
```

---

**Schülerverwaltung**

```markdown
+--------------------------------------+
|          Schülerverwaltung           |
+--------------------------------------+
| [Einfügen]  [Suchen]                 |
+--------------------------------------+
| Suchfeld: [______________________]    |
+--------------------------------------+
| Schülerliste:                        |
|  Nachname       | Vorname           | Ausweisnummer |           |
| --------------- | ----------------- | ------------- |-----------|
| Mustermann      | Max               | 10001         | [Bearbeiten] [Löschen] |
| Musterfrau      | Erika             | 10002         | [Bearbeiten] [Löschen] |
+--------------------------------------+
| Klicke auf "Bearbeiten" für Änderungen oder "Löschen" zum Entfernen |
+--------------------------------------+
```

**Einfügeformular für Schüler**

```markdown
+--------------------------------------+
|         Schüler hinzufügen           |
+--------------------------------------+
| Nachname:         [__________]       |
| Vorname:          [__________]       |
| Ausweisnummer:    [__________]       |
+--------------------------------------+
|             [Speichern]              |
+--------------------------------------+
```

---

**Ausleihe / Rückgabe**

```markdown
+-------------------------------------+
|         Ausleihe / Rückgabe         |
+-------------------------------------+
| Aktion:  ( ) Ausleihe   ( ) Rückgabe |
+-------------------------------------+
| Schülerausweisnummer: [__________]   |
+-------------------------------------+
| Bücher hinzufügen:                   |
| Buchnummer:       [__________] [Add] |
+-------------------------------------+
| Ausgewählte Bücher:                  |
| - Einführung in C# (00001)           |
| - Der Prozess (00002)                |
| - Krieg und Frieden (00003)          |
+-------------------------------------+
| Verifizierung:                       |
|   Schülername:    Max Mustermann     |
+-------------------------------------+
|             [Bestätigen]             |
+-------------------------------------+

```

---

**Statistik**

```markdown
+-------------------------------------+
|               Statistik             |
+-------------------------------------+
| Monatliche Ausleihen:               |
|  Januar      120                    |
|  Februar     100                    |
|  März        130                    |
+-------------------------------------+
| Durchschnittliche Ausleihdauer:     |
|  14 Tage                            |
+-------------------------------------+
| Häufigste Fachgebiete:              |
|  1. Informatik                      |
|  2. Mathematik                      |
|  3. Geschichte                      |
+-------------------------------------+
| Meist ausgeliehene Bücher:          |
|  1. Einführung in C#      35x       |
|  2. Der Prozess           28x       |
|  3. Moby Dick             25x       |
|  4. Die Odyssee           20x       |
|  5. Faust                 18x       |
+-------------------------------------+
|         [Zurück zum Hauptmenü]      |
+-------------------------------------+

```

---

**Ausgeliehene Bücher**

```markdown
+---------------------------------------------+
|            Ausgeliehene Bücher              |
+---------------------------------------------+
| Suchfeld: [__________________________]       |
+---------------------------------------------+
| Buchnummer  | Titel              | Ausgeliehen von | Ausleihdatum | Fristdatum  |
|------------ | ------------------ | --------------- | ------------ | ----------- |
| 00001-2024  | Einführung in C#   | Max Mustermann | 2024-10-01   | 2024-10-15  |
| 00002-2024  | Der Prozess        | Anna Schmidt   | 2024-09-20   | 2024-10-04  |
| 00003-2024  | Moby Dick          | Paul Müller    | 2024-09-30   | 2024-10-14  |
| ...         | ...                | ...            | ...          | ...         |
+---------------------------------------------+
|         [Zurück zum Hauptmenü]              |
+---------------------------------------------+

```

---

Export Fenster

```markdown
+---------------------------------------------+
|         Import / Export von Daten           |
+---------------------------------------------+
| Exportieren:                                |
| [ ] Bücher                                  |
| [ ] Schüler                                 |
| [ ] Ausleihen                               |
| Datei Format: [ CSV ▼]                      |
+---------------------------------------------+
|           [Exportieren]                     |
+---------------------------------------------+
| Importieren:                                |
| Datei auswählen: [____________________] [Durchsuchen] |
| Datei Format: [ CSV ▼]                      |
+---------------------------------------------+
|           [Importieren]                     |
+---------------------------------------------+
|         [Zurück zum Hauptmenü]              |
+---------------------------------------------+


```