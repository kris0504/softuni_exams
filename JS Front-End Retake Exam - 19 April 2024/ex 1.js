function solve(input){
    let code=input.shift();
    let command=input.shift();
    while (command!='End') {
        const [cmd,f,s]=command.split('!');
    if (cmd=='RemoveEven') {
        let newcode='';
        for (let index = 0; index < code.length; index+=2) {
            newcode+=code[index];
        }
        code=newcode;
        console.log(code);
    }else if (cmd=='TakePart') {
        let sub=code.substring(f,s)
        code=sub;
        console.log(code);
    }else if(cmd=='Reverse'){
        if(code.includes(f)){
            code=code.replace(f,'');
            var newString = "";
              for (var i = f.length - 1; i >= 0; i--) {
              newString += f[i];
                }
                

            code+=newString;
            console.log(code)
            
        }else{
            console.log('Error')
        }
    }
    command=input.shift();
    }
    console.log(`The concealed spell is: ${code}`)
}