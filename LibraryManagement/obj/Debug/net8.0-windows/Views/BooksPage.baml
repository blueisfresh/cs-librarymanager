   M S B A M L   `   `   `  ����  LibraryManagement'    !LibraryManagement.Views.BooksPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Bücherverwaltung��5      $	��500��6    $	��800��6-   .��5      � $��20q�6   .+�5   
   �� $��Bücherverwaltung��6   $
��Bold"�6,   $��20(�6>   $��Center=�6L   $	��Top=�6i   $��	0,10,0,20q�6�   �� 5
   
    �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $	��Top=�6I   $��	10,40,0,0q�6a   .+�5      �� $��	Einfügen��6   $	��100��6(   $��0,0,10,0q�64   ��6F   �� *OpenAddBooksWindowCommand+�� 5      $��Suchen��6   $	��100��6&   $��0,0,10,0q�62   ��6D   �� *SearchCommand+5      �� 5   
   $F��@Hinweis: Sie können nach dem Titel suchen, nicht nach der ISBN.��6   
�������$��Italic$�5   &   $��	10,70,0,0q�69   �� 5   
   $ 
Horizontal=�6   $	��Top=�6.   $��
10,100,0,0q�6F   .+�5      �� $��	Suchfeld:��6   $��Center=�6)   $��0,0,10,0q�6D   �� 5      $	��300��6   ��6"   �� *
SearchTerm+ �� UpdateSourceTrigger$ PropertyChanged=�5      &   System.Windows.Controls.DataGrid  5   
   $��10,140,10,20q�5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5      ��5      �� *Books+ �� SelectedItem 5      �� *SelectedBook+ �� Mode$ TwoWay=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$ 
Buchnummer��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 100 6U   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 69   �� *	BookNum+6U     5!      $ Titel��6%   $		 200 6N   
 64   �� *Title+6N     5"      $ Autor��6%   $		 150 6O   
 64   �� *Author+6O     5#      $ Verlag��6%   $		 150 6S   
 65   �� *	Publisher+6S     5$      $
 ISBN��6%   $		 150 6L   
 63   �� *ISBN+6L   4  .System.Windows.Controls.DataGridTemplateColumn  5'      $ Aktion��6)   $		 150 69      CellTemplate 5(      �� 5)      .R�5*      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5,   "   �� $��
Bearbeiten��6)   $	��5,0q�5/   )   ��5-   )   �� *DataContext.EditBookCommand+ �� RelativeSource ��  �� AncestorType DataGrid��5.   )   �� 5/   )   �� 52   "   $��Löschen��6)   $	��5,0q�55   )   ��53   )   �� *)'DataContext.OpenDeleteBookWindowCommand+ ��  DataGrid��54   )   �� 55   )   56      57      59      5;      5<      5=      