   M S B A M L   `   `   `  ����=  9bd, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:bdbd  5   	       bd.ResultWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:bd     5   	     � Title$;  5Неотсканированный инвентарь��5   	   $	��450��6-   $	��800��6:   .��5	      � .+�5
   
   �� .@�5      �� $%��Не отсканировано��6   ��5      �� 5      .+�5      o� .@�5      m� -   5        $��PrintMenuBtn��5   B   $!��Сохранить в Excel��6'   6V   m� 5      -   5        $��PDFBtn��5   @   $��Сохранить в PDF��6'   6N   5      �� 5      .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ 
ResultGrid��5   '      AutoGenerateColumns .  $	��370��6W      
IsReadOnly .    HeadersVisibility$ Column=�6v      Columns 5   "   0  *System.Windows.Controls.DataGridTextColumn  5   &   ,  &System.Windows.Controls.DataGridColumn   Header$( "Инвентраный  Номер��69      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6}   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6U   �� 	 �� Path$	 inventory_code�6}     5   &   $ Название��69   $
 auto 6j    6K   �� $
	 Name�6j     5   &   $ Цена��69   $
 auto 6g    6G   �� $	 Price�6g     5   &   $ Количество��69   $
 auto 6n    6M   �� $	 Amount�6n     5   &   $( "Месторасположение��69   $
 auto 6�    6T   �� $#	 Workplaces.Locations.Location�6�     5   &   $ Рабочее место��69   $
 auto 6z    6P   �� $	 Workplaces.Place�6z     5   &   $) #Ответственное лицо��69   $
 auto 6�    6U   �� $	 Responsible_Persons.Name�6�   5      5      5      5!      �� 5"      $ ��Отсканировано��6   .��5#      �� .+�5$      o� .@�5%      m� -   5        $��PrintMenuBtn2��5%   >   $!��Сохранить в Excel��6#   6S   m� 5&      -   5        $��PDFBtn2��5&   <   $��Сохранить в PDF��6#   6K   5'      �� 5(      .+�5)        -   5        $ ResultGrid2��5)   #    .  $	��370��6T    . $ Column=�6s    5*        5+   "   $( "Инвентраный  Номер��65   $
 auto 6y    6Q   �� $	 inventory_code�6y     5,   "   $ Название��65   $
 auto 6f    6G   �� $
	 Name�6f     5-   "   $ Цена��65   $
 auto 6c    6C   �� $	 Price�6c     5.   "   $ Количество��65   $
 auto 6j    6I   �� $	 Amount�6j     5/   "   $( "Месторасположение��65   $
 auto 6�    6P   �� $#	 Workplaces.Locations.Location�6�     50   "   $ Рабочее место��65   $
 auto 6v    6L   �� $	 Workplaces.Place�6v     51   "   $) #Ответственное лицо��65   $
 auto 6�    6Q   �� $	 Responsible_Persons.Name�6�   53      54      55      56      57      59      5:      