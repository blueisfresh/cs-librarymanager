   M S B A M L   `   `   `  ����  LibraryManagement.    (LibraryManagement.Views.ImportExportPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Import / Export von Daten��5      $	��500��6)   $	��400��66   .��5      � $��20q�6   .+�5   
   �� $��Import / Export von Daten��6   $
��Bold"�65   $��20(�6G   $��Center=�6U   $	��Top=�6r   $��	0,10,0,20q�6�   �� 5
   
   $	��Top=�6   $��	10,60,0,0q�6-   .+�5      �� $��Exportieren:��6   $
��Bold"�6,   $��0,0,0,5q�6>   �� 5      $��Bücher��6   $��10,0,0,0q�6(    d� 	IsChecked 6:   �� *ExportBooks+�� 5      $��Schüler��6   $��10,0,0,0q�6)    6;   �� *ExportStudents+�� 5       �� Orientation$ 
Horizontal=�6   $��0,10,0,0q�62   .+�5      �� $��Datei Format:��6   $��Center=�61   $��0,0,10,0q�6L   �� 5      $
��JSON��6   $��Center=�6(   5      �� 5      $��Exportieren��6   $	��100��6+   $��Center=�67   $��0,10,0,0q�6T   ��5      �� *ExportCommand+5      �� 5   
   $	��Top=�6   $��
10,240,0,0q�6-   .+�5      �� $��Importieren:��6   $
��Bold"�6,   $��0,0,0,5q�6>   �� 5       $ 
Horizontal=�6   $��0,10,0,0q�62   .+�5!      �� $��Importieren als:��6   $��Center=�64   $��0,0,10,0q�6O   �� 5"      $	��100��6    �� SelectedItem 6'   �� *SelectedImportOption+ �� Mode$ TwoWay=�.@�5#      �� $��Bücher��6#   �� 5$      $��Schüler��6#   5%      5'      �� 5*      $ 
Horizontal=�6   $��0,10,0,0q�62   .+�5+      �� $��Datei auswählen:��6   $��Center=�64   $��0,0,10,0q�6O   �� 5,      $	��150��6    �� 
IsReadOnly$
 True��6&   ��68   �� *SelectedImportFilePath+�� 5-      $��Durchsuchen��6   $��80��6/   $��5,0,0,0q�6:   ��5.      �� *ImportCommand+5/      �� 52      $ 
Horizontal=�6   $��0,10,0,0q�62   .+�53      �� $��Datei Format:��6   $��Center=�61   $��0,0,10,0q�6L   �� 54      $
��JSON��6   $��Center=�6(   55      �� 58      $��Importieren��6   $	��100��6+   $��Center=�67   $��0,10,0,0q�6T   ��59      �� *ImportCommand+5:      �� 5=   
   $��Zurück zum Hauptmenü��6   $	��180��60   $��Center=�6<   $��Bottom=�6Y   $��0,10,0,0q�6t   5>      5?      