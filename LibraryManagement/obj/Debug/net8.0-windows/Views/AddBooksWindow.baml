   M S B A M L   `   `   `  ����  LibraryManagement,    &LibraryManagement.Views.AddBooksWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Buch hinzufügen��5   	   $	��400��6!   $	��400��6.   .��5      � $��20q�6   .+�5   
   �� $��Buch hinzufügen��6   $
��Bold"�6+   $��20(�6=   $��Center=�6K   $	��Top=�6h   $��	0,10,0,20q�6�   � 5
   
   $��0,60,0,0q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $	��150 �6#   �� 5      $��* �6#   .+�5      �� $��Buchnummer:��6   $��Center=�6+   $	��0,5q�6F   �� 5      $��1��6   $��5q�6&   ��61   �� *NewBook.BookNum+ �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors$
 True���� 5      $��Titel:��6   $��1��6&   $��Center=�63   $	��0,5q�6N   �� 5       $��1��6   $��1��6#   $��5q�63   ��6>   �� *NewBook.Title+$ PropertyChanged=�$
 True���� 5#      $��Autor:��6   $��2��6&   $��Center=�63   $	��0,5q�6N   �� 5$      $��2��6   $��1��6#   $��5q�63   ��6>   �� *NewBook.Author+$ PropertyChanged=�$
 True���� 5'      $��Verlag:��6   $��3��6'   $��Center=�64   $	��0,5q�6O   �� 5(      $��3��6   $��1��6#   $��5q�63   ��6>   �� *NewBook.Publisher+$ PropertyChanged=�$
 True���� 5+      $��ISBN:��6   $��4��6%   $��Center=�62   $	��0,5q�6M   �� 5,      $��4��6   $��1��6#   $��5q�63   ��6>   �� *NewBook.ISBN+$ PropertyChanged=�$
 True���� 5/      $��Verlagsort:��6   $��5��6+   $��Center=�68   $	��0,5q�6S   �� 50      $��5��6   $��1��6#   $��5q�63   ��6>   �� *NewBook.PublicationPlace+$ PropertyChanged=�$
 True���� 53      $��Erscheinungsdatum:��6   $��6��62   $��Center=�6?   $	��0,5q�6Z   (  "System.Windows.Controls.DatePicker  54      $��6��6   $��1��6&   $��5q�66      SelectedDate 6A   �� *NewBook.PublicationDate+$ PropertyChanged=�$
 True��55      �� 58   
   $��	Speichern��6   $	��100��6%   $��Center=�61   $��Bottom=�6N   $��0,10,0,0q�6i   ��6{   �� *SaveBookCommand+59      5:      