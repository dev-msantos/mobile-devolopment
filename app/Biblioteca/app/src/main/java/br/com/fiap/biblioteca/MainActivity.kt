package br.com.fiap.biblioteca

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import br.com.fiap.biblioteca.data.model.Account
import br.com.fiap.biblioteca.data.model.FormLogin
import br.com.fiap.biblioteca.data.model.GenericResponse
import br.com.fiap.biblioteca.data.remote.RetrofitBuilder
import retrofit2.*

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val btnLogin = findViewById<Button>(R.id.btn_login)
        val btnCreateAccount = findViewById<Button>(R.id.btn_create_new_account)

        btnLogin.setOnClickListener {
            val campoUserName = findViewById<EditText>(R.id.user_name);
            val campoPassword = findViewById<EditText>(R.id.password);
            val userName = campoUserName.text.toString().trim();
            val password = campoPassword.text.toString().trim();

            val form = FormLogin(userName, password)
            val callLogin = RetrofitBuilder().authService.login(form)

            if(userName.isNotEmpty() && password.isNotEmpty())
            {
                callLogin.enqueue(object : Callback<GenericResponse<Account>?> {
                    override fun onResponse(
                        call: Call<GenericResponse<Account>?>,
                        response: Response<GenericResponse<Account>?>
                    ) {
                        if (response.code().equals(200))
                        {
                            Toast.makeText(applicationContext, response.body()?.responseMessage, 3000).show()
                            openCatalogBook()
                        } else {
                            Toast.makeText(applicationContext, response.body()?.responseMessage, 3000).show()
                        }
                    }

                    override fun onFailure(call: Call<GenericResponse<Account>?>, t: Throwable) {
                        Toast.makeText(applicationContext, t.message, 3000).show()
                    }
                })
            }
            else {
                Toast.makeText(applicationContext, "Login e senha são obrigatórios", 3000).show()
            }
        }

        btnCreateAccount.setOnClickListener {
            openActivityAccount()
        }

    }

    private fun openActivityAccount() {
        val intent = Intent(this, AccountActivity::class.java)
        startActivity(intent);
    }

    private fun openCatalogBook() {
        val intent = Intent(this, CatalogBookActivity::class.java)
        startActivity(intent);
    }

}