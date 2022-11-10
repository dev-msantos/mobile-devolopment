package br.com.fiap.biblioteca

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import br.com.fiap.biblioteca.data.model.Account
import br.com.fiap.biblioteca.data.model.FormCreateAccount
import br.com.fiap.biblioteca.data.model.GenericResponse
import br.com.fiap.biblioteca.data.remote.RetrofitBuilder
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class AccountActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_account)

        val btnCreateAccount = findViewById<Button>(R.id.btn_create_new_account)

        btnCreateAccount.setOnClickListener {
            val campoUserName = findViewById<EditText>(R.id.user_name)
            val campoName = findViewById<EditText>(R.id.name)
            val campoPassword = findViewById<EditText>(R.id.password)
            val campoConfirmPassword = findViewById<EditText>(R.id.confirm_password)

            val userName = campoUserName.text.toString().trim()
            val name = campoName.text.toString().trim()
            val password = campoPassword.text.toString()
            val confirmPassword = campoConfirmPassword.text.toString()

            val form = FormCreateAccount(userName, name, password, confirmPassword)
            Log.i("Form Create Account: ", form.toString())
            val call = RetrofitBuilder().authService.createAccount(form)

            if(userName.isNotEmpty() && name.isNotEmpty() && password.isNotEmpty() && confirmPassword.isNotEmpty())
            {
                call.enqueue(object : Callback<GenericResponse<Account>?> {
                    override fun onResponse(
                        call: Call<GenericResponse<Account>?>,
                        response: Response<GenericResponse<Account>?>
                    ) {
                        if(response.code().equals(201) || response.equals(200))
                        {
                            Toast.makeText(applicationContext, response.body()?.responseMessage, 3000).show()
                            openMainActivity()
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
                Toast.makeText(applicationContext, "Todos os campos são obrigatórios", 3000).show()
            }

        }

    }

    private fun openMainActivity() {
        val intent = Intent(this, MainActivity::class.java)
        startActivity(intent);
    }
}