   M S B A M L   `   `   `  ����  LibraryManagement.    (LibraryManagement.Views.BorrowReturnPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Ausleihe / Rückgabe��5      $	��500��6#   $	��400��60   .��5      � $��20q�6   .+�5   
   �� $��Ausleihe / Rückgabe��6   $
��Bold"�6/   $��20(�6A   $��Center=�6O   $	��Top=�6l   $��	0,10,0,10q�6�   �� 5   
    �� Orientation$ 
Horizontal=�6   $	��Top=�6.   $��	10,50,0,0q�6F   .+�5	      �� $��Aktion:��6   $��Center=�6'   $��0,0,10,0q�6B   	� 5
      $��Ausleihe��6    	� 	GroupName$ Action��6-   $��0,0,10,0q�6b    d� 	IsChecked 6@   �� *IsBorrowing+6b   	� 5      $��	Rückgabe��6   $ Action��6-    6@   �� *IsReturning+5      �� 5   
   $ 
Horizontal=�6   $	��Top=�6.   $��
10,100,0,0q�6F   y�6Z   �� *IsReturningVisible+.+�5      �� $��Schülerausweisnummer:��6   $��Center=�65   $��0,0,10,0q�6P   �� 5      $	��200��6   ��6"   �� *StudentLibraryCardNum+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5   
   $	��Top=�6   $��
10,120,0,0q�6-   .+�5      �� $��Bücher hinzufügen:��6   $
��Bold"�62   $��0,0,0,5q�6D   �� 5      $ 
Horizontal=�6   .+�5      �� $��Buchnummer:��6   $��Center=�6/   $��0,0,10,0q�6J   �� 5      $	��100��6   $��0,0,5,0q�6&   ��67   �� *
BookNumber+$ PropertyChanged=��� 5      $	��Add��6   $��50��6'   ��62   �� *AddBookCommand+5      5      �� 5   
   $	��Top=�6   $��
10,180,0,0q�6-   .+�5      �� $��Ausgewählte Bücher:��6   $
��Bold"�63   $��0,0,0,5q�6E   �� 5       �� DisplayMemberPath$ Title��6<   $��80��6V   ��6   �� *SelectedBooks+6V   5      �� 5!   
   $��Bestätigen��6   $	��100��6&   $��Center=�62   $��Bottom=�6O   $��0,10,0,0q�6j   ��6|   �� *ConfirmActionCommand+5"      5#      