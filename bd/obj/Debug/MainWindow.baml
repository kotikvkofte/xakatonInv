   M S B A M L   `   `   `  ����=  9bd, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:bdbd  5   	       bd.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:bd     5   	     � Title$"  Инвентаризация��5   	   $	��450��6    $	��800��6-    � WindowStartupLocation$ CenterScreen=�69    +� FocusedElement 5	   	   ��  �� ElementName$ 
BarcodeTxb��.��5
      �  � ColumnDefinitions 5   
   �� 5      $
��173* �6   �� 5      $
��223* �6   .+�5   
   o� $	��Top=�6   $��2��6'   .@�5      m� $��Добавить��6   .@�5      m� -   5        $��AddLocationBtn��5   +   $��Отделы��6   6A   m� 5      -   5        $��AddWorkplaceBtn��5   2   $��Рабочие места��6   6I   m� 5      -   5        $��AddRepPersonBtn��5   7   $)��#Ответственные лица��6   6N   5      m� 5      -   5        $��UpdateDBBtn��5   H   $��Обновить БД��6   6H   m� 5      -   5        $��OpenListBtn��5   I   $!��Открыть список��6   6I   5      (� 5   
   $��20q�6   .��5      �� $��Center=�6   $��Center=�5       �� Orientation$ Vertical=�5       5      �� $ 
BarcodeTxb��.+�5       �� $ Vertical=�6    K� ZIndex$ 1��64    5!      �� $ 
BarcodeTxb��.+�5"      ��  �� TextAlignment$ Center=�5#      $
��12pt(�5$      $��5q�5%      Инвентарный�� 63   
номер5&      �� 5'      -   5        $��
BarcodeTxb��5,      $	��200��5'      $��30��5(       �� 	MaxLength$ 30��5)      $
��14pt(�5*      	 �� TextAlignment$	 Center=�5+      
���   �$��1q�5.      |�. 
 +� IsFocusScope
 . .��52   ?    � ContextMenu 53      �� 54      .@�55   "   m� -   5        $��AddInventory��55   =   $��Добавить��6+   6Q   56      .��57   /   58      �� 59      -   5         ��Name$ 
SelectChkb��5:      $ ��Автовыделение��59      $��Center=�5;      5=      �� 5>      -	   5        $��ScanCodeImg��5?      $��	0,10,0,10q�5>      $	��200��5@      $��70��5A       5B      �� 5C      .@�5D   "   m� -
   5        $(��"Добавить на печать��5D   +   6G   5E      5G      5H      �� 5I      $��Center=�6   $��Bottom=�5J      .+�5K      �� -   5        $��InventoryzationButton��5M      $1��+ Начать инвентаризацию ��5K      $
��12pt(�5L      $��0,5,0,5q�5O      �� 5P      -   5        $#��InventoryzationContinueButton��5R      $9��3 Продолжить инвентаризацию ��5P      $
��12pt(�5Q      $��0,5,0,5q�5T      �� 5U      -   5        $��ChkbsInventoryPanel��5U   k   $ 
Horizontal=�6!   $��Center=�6:   $y�Hidden=�6W   .+�5V      �� -   5        $��InventoryCurrentTxt��5V   -   $��0��6$   6-   �� 5W      $�� из ��6$   �� 5X      -   5        $��InventoryTotalTxt��5X   -   $��0��6$   6-   5Y      5Z      5[      5\      (� 5]   
   $��1��6   .��5^      �� $��Center=�6   $	��300��64   .+�5_      �� -   5         ��Name$ 
InputPanel��5_      $ 0��61   .+�5`      �� -   5        $��InputMsg��5c       $6��0Введите инвентарный номер��5`       $��0,12,0,0q�5a       $ Center=�5b       $
��12pt(�5d       5f      �� 5g      -   5        $��	MainPanel��5j      $��Center=�5g      $��Center=�5h      $y�Hidden=�5i      $ 1��5k      .+�5l      �� $ Vertical=�6!   .+�5m      �� $��Название:��6$   $ Center=�5n       $
��12pt(�5o       �� 5p      -   5         ��Name$ NameTxt��5p   $   $ Center=�5q       $
��12pt(�5r       $
��Wrap=�5s       $��5q�5t       5u      �� 5v      $ Vertical=�6!   $��5q�68   .+�5w      �� $)��#Месторасположение:��6$   $ Center=�5x       $
��12pt(�5y       �� 5z      -   5        $��LocationCmb��5z   #   $��	0,10,0,10q�66   $
��12pt(�5|   #   5~      5      �� 5�      $ Vertical=�6!   $��5q�68   .+�5�      �� $ ��Рабочее место:��6$   $ Center=�5�       $
��12pt(�5�       �� 5�      -   5        $��WorkplaceCmb��5�   #   $��	0,10,0,10q�67   {�.  $
��12pt(�5�   #   5�      5�      �� 5�      $ Vertical=�6!   $��5q�68   .+�5�      �� $*��$Ответственное лицо:��6$   $ Center=�5�       $
��12pt(�5�       �� 5�      -   5        $��
RespPerson��5�   #   $��	0,10,0,10q�65   $
��12pt(�5�   #   5�      5�      �� 5�      $ 
Horizontal=�6!   $��Center=�5�      .+�5�      �� $ Vertical=�6%   $��15q�6<   .+�5�      �� $��Количество:��6(   $ Center=�5�       $
��12pt(�5�       �� 5�      -   5        $ 	AmountTxt��5�   (   $ Center=�5�       $
��12pt(�5�       $��5q�5�       5�      �� 5�      $ Vertical=�6%   $��15q�6<   .+�5�      �� $��	Цена:��6(   $ Center=�5�       $
��12pt(�5�       �� 5�      -   5        $ PriceTxt��5�   (   $ Center=�5�       $
��12pt(�5�       $��5q�5�       5�      5�      5�      5�      5�      5�      5�      