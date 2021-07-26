format PE GUI 4.0 DLL

include 'win32a.inc'

; funkcje eksportowane oraz wewnêtrzne
section '.text' code readable executable



proc tran_x c, x:QWORD, y:QWORD, a:QWORD, b:QWORD, c:QWORD

push ebx
       finit
      ; fld [const_2]
       fld [x]
       fld [a]
       fmulp
       fld [y]
       fld [b]
       fmulp
       faddp
       fld [c]
       faddp

        pop ebx

        ret
endp
proc tran_y c, x:QWORD, y:QWORD, d:QWORD, e:QWORD, f:QWORD

push ebx
       finit

       fld [x]
       fld [d]
       fmulp
       fld [y]
       fld [e]
       fmulp
       faddp
       fld [f]
       faddp

        pop ebx

        ret
endp

proc xe c, x:QWORD, width:QWORD,x_min:QWORD,x_max:QWORD

push ebx
       finit

       fld [x]
       fld [x_min]
       fsubp
       fld [x_max]
       fld [x_min]
       fsubp
       fdivp
       fld [width]
       fmulp

        pop ebx

        ret
endp
proc ye c, y:QWORD,height:QWORD,y_min:QWORD,y_max:QWORD

push ebx
       finit

       fld [y]
       fld [y_min]
       fsubp
       fld [y_max]
       fld [y_min]
       fsubp
       fdivp
       fld [const_1]
       fmulp
       fld [height]
       fmulp
       fld [height]
       faddp


        pop ebx

        ret
endp
proc generator c, los:QWORD,a:DWORD,b:DWORD,m:DWORD

push ebx
local temp:QWORD
       finit

       fild [a]
       fld [los]
       fmulp
       fild [b]
       faddp
       fstp [temp]
       finit
       fild [m]
       fld [temp]
       fprem




        pop ebx

        ret
endp
proc normalizuj c, los:QWORD

push ebx

       finit
       fild [const_100]
       fld [los]
       fprem


        pop ebx

        ret
endp







;

section '.data' data readable writeable
           _tytul db "OK", 0
           _tresc db "Biblioteka MojaDLL.dll jest dostêpna", 0
            const_1 dq -1.0

             const_100 dd 100




section '.idata' import data readable writeable

 library user32,'USER32.DLL'

 import  user32,\
   MessageBoxA,'MessageBoxA'


section '.edata' export data readable

  export 'MojaDLL.DLL',\
         tran_x,'tran_x',\
         tran_y,'tran_y',\
         xe,'xe',\
         ye,'ye',\
         generator,'generator',\
         normalizuj, 'normalizuj'


section '.reloc' fixups data readable discardable
