   M S B A M L   `   `   `  ����  LibraryManagement/    )LibraryManagement.Views.BorrowedBooksPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Ausgeliehene Bücher��5      $	��500��6#   $	��600��60   .��5      � $��20q�6   .+�5   
   �� $��Ausgeliehene Bücher��6   $
��Bold"�6/   $��20(�6A   $��Center=�6O   $	��Top=�6l   $��	0,10,0,20q�6�   �� 5   
    �� Orientation$ 
Horizontal=�6   $	��Top=�6.   $��	10,60,0,0q�6F   .+�5	      �� $��	Suchfeld:��6   $��Center=�6)   $��0,0,10,0q�6D   �� 5
      $	��300��6   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��BorrowedBooksDataGrid��5      $��10,100,10,60q�6V      AutoGenerateColumns$ False��6l      
IsReadOnly$
 True��6�   ��60   �� *BorrowedBooks+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Buchnummer��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *
Buchnummer+6X     5      $ Titel��6%   $	 150 6N    64   �� *Titel+6N     5      $ Ausgeliehen von��6%   $	 150 6a    6>   �� *AusgeliehenVon+6a     5      $ Ausleihdatum��6%   $	 100 6\    6;   �� *Ausleihdatum+6\     5      $ 
Fristdatum��6%   $	 100 6X    69   �� *
Fristdatum+6X   5      �� 5   
   $��Zurück zum Hauptmenü��6   $	��180��60   $��Center=�6<   $��Bottom=�6Y   $��0,10,0,0q�6t   5      5      