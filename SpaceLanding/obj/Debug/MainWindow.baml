   M S B A M L   `   `   `  ����A  =Lander, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SpaceLandingSpaceLanding  5   	       SpaceLanding.MainWindow   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   8x,http://schemas.microsoft.com/winfx/2006/xaml    5   	   >d2http://schemas.microsoft.com/expression/blend/2008    5   	   Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5   	   *localclr-namespace:SpaceLanding    5   	     � Title$  
Lander 0.1��5   	   $	��476��6   $	��724��6)    � WindowStartupLocation$ CenterScreen=�65    � 
ResizeMode$ NoResize=�6Z   .��5	      � .+�5
   
   �� -   5         ��Name$ groupBox��5
      $%��Настройки полёта��6%   $
��Left=�6?   $��	10,10,0,0q�6Z   $	��Top=�6m   $	��165��6�   $	��685��6�   .��5      � $
��Left=�6   $��136.96��6.   $��
4,3.04,0,0q�6>   $	��Top=�6R   $	��671��6j   .+�5      �� -   5         ��Name$ SpaceLands_comboBox��5      $
��Left=�68   $��
155,14,0,0q�6S   $	��Top=�6g   $	��145��6   6�   �� 5      -   5         ��Name$ label��5      $(��"Куда приземляемся:��6'   $
��Left=�6D   $��	10,10,0,0q�6_   $	��Top=�6r   �� 5      -   5        $ SpaceCrafts_comboBox��5      $
��Left=�69   $��155,44.96,0,0q�6T   $	��Top=�6k   $	��145��6�   6�   �� 5      -   5        $ 
label_Copy��5      $+��%На чём приземляемся:��6,   $
��Left=�6K   $��10,40.96,0,0q�6f   $	��Top=�6|   �� 5      -   5        $! SpaceLand_Description_Label��5      $��Desc.��6=   $
��Left=�6M   $��
305,10,0,0q�6h   $	��Top=�6|   �� 5      -   5        $" SpaceCraft_Description_Label��5      $��Desc.��6>   $
��Left=�6N   $��305,40.96,0,0q�6i   $	��Top=�6�   5      5      �� 5   
   -   5         ��Name$ 	Go_button��5      $��Поехали!��6$   $
��Left=�67   $��
10,180,0,0q�6R   $	��Top=�6f   $	��685��6~   $��25��6�   6�   �� 5   
   -	   5        $ label2��5      $"��Подача топлива:��6    $
��Left=�6:   $��152.393,400,0,0q�6U   $	��Top=�6n   �� 5   
   -
   5         ��Name$ KgToExhaust_textBox��5      $
��Left=�6/   $��23��6J   $��259.06,404,0,0q�6V    �� TextWrapping$
 Wrap=�6n   $	��Top=�6�   $	��120��6�   {�.  6�   �� 5   
   -   5        $ label2_Copy��5      $7��10-100 кг/с (в течение 10 секунд)��6%   $
��Left=�6P   $��384.06,401.04,0,0q�6k   $	��Top=�6�   �� 5   
   -   5        	 ��Name$	 Status_textBlock��5      $
��Left=�6.   $��
10,210,0,0q�6I   $��WrapWithOverflow=�6]   $	��Top=�6}   $	��685��6�   5      5      