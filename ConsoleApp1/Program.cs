using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        

        static void Main(string[] args)
        {
            string privat_key = "sandbox_qA9vPxImZvOnyKPie2jksfVKdg0UPDUTwEm3Amvw";
            string public_key = "sandbox_i67125587549";
            Dictionary<string, string> json_dict = new Dictionary<string, string>(5)
            {
                {"public_key", public_key},//Тут думаю и так все понятно, мог написать вручную, но решил продемонстрировать как это работает с переменной
                {"version","3"},//Версия API, это лучше не трогать, по идее от LiqPay должна быть информация как только они сделают новую версию
                {"action","pay"},//Тип операции. Возможные значения: pay - платеж, hold - блокировка средств на счету отправителя, subscribe - регулярный платеж, paydonate - пожертвование, auth - предавторизация карты
                {"amount","666"}, //Сумма платежа. Сюда вместо 999 нужно вставить переменную с общей суммой из корзины
                {"currency","USD"}, //Может быть USD, EUR, RUB, UAH, BYN, KZT
                {"description","test" }, //Назначения платежа, сюда можно вписать что угодно в правой части вместо "test", например "Оплата за товар" и более того, привязать вместо числа переменную и вставлять за какой конкретно товар оплата или даже список товара
                {"order_id","000002" }//То что я говорил нужно проходится циклом после каждого нажатия кнопки
            };
            //Ниже создание из словаря нужную нам строку
            string json_string ="";
            foreach(var pair in json_dict)
            {
                //Console.Write("\"{0}\":\"{1}\",", pair.Key, pair.Value);
                json_string = json_string + "\"" + pair.Key +"\"" + ":" +"\"" + pair.Value +"\"" +",";
                
            }
            json_string = json_string.Remove(json_string.Length - 1);
            json_string = "{" + json_string + "}";
            //До этой строки мы закончили с обработкой исходной строки
            string data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json_string)); //конвертирование в Base64 (это и есть наша ДАТА которую мы вставляем в форму)
            string sing_string = privat_key + data + privat_key; //Это страка которую мы обратаываем чтобы получить сигнатуру
            var signature = Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(sing_string)));
            //Строка выше это обработка сигнатуры, сначала шифруем в SHA1 потом в Base64
            //Ниже я просто вывожу для себя дату и сигнатуру, чтобы вручную вставить в форму html, вы же просто сделаете из этого кода контроллер оплаты, и форма вставляется ВМЕСТО КНОПКИ "оплатить" которая у вас была ранее
            Console.WriteLine(data);
            Console.WriteLine(signature);
            //Ниже будет форма куда нужно вписать нужные значения и вставить на сайт

            /*< form method = "POST" action = "https://www.liqpay.ua/api/3/checkout" accept - charset = "utf-8" >
     
      < input type = "hidden" name = "data" value = @data />
          
           < input type = "hidden" name = "signature" value = @signature />
               
                < input type = "image" src = "//static.liqpay.ua/buttons/p1ru.radius.png" />  //Эта строка это картинка кнопки, почему-то у меня она проходит как битая ссылка
                  </ form >
            */



        }
        
            
        
    }
}
