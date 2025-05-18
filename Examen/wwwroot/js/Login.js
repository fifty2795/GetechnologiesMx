//#region Selectors

const selectors = {
    nombre: '#txtNombre',
    identificacion: '#txtIdentificacion'    
};

//#endregion

//#region Login Manager

const LoginManager = {
    init: function () {
        const btnLogin = document.querySelector('#btnLogin');        
        const btnRegistrarse = document.querySelector('#btnRegistrarse');        

        if (btnLogin) btnLogin.addEventListener('click', this.loginUsuario);        
        if (btnRegistrarse) btnRegistrarse.addEventListener('click', this.registrarUsuario);        
    },

    loginUsuario: function () {
        if (!validarFormulario('Login')) return;

        const nombre = $(selectors.nombre).val();
        const identificacion = $(selectors.identificacion).val();

        const data = {
            nombre: nombre,
            identificacion: identificacion
        }

        $.ajax({
            url: '/Login/Login',
            method: 'POST',
            data: data,
            beforeSend: function () {
                $('#loading').show();
            },
            success: function (response) {
                setTimeout(() => {
                    $('#loading').hide();
                }, 2000);

                if (response.success) {
                    window.location.href = response.redirectUrl;
                } else {
                    mostrarAlertaAlertify(null, response.message, 'warning');
                }
            },
            error: function (xhr, status, error) {
                setTimeout(() => {
                    $('#loading').hide();
                }, 2000);
                console.error("Error AJAX en AjaxRequest:", xhr.responseText + ' ' + error);
                mostrarAlertaAlertify(null, 'Ocurrio un error al iniciar sessión', 'error');
            }
        });
    }  
};

//#endregion

//#region Validaciones

const validarFormulario = (tipo) => {

    const campos = [
        { id: $(selectors.nombre), mensaje: 'Nombre es requerido' },
        { id: $(selectors.identificacion), mensaje: 'Identificacion es requerido' }
    ];

    for (const campo of campos) {
        const $input = $(campo.id);
        const valor = $input.val().trim();

        if (valor === '') {
            mostrarAlertaAlertify(campo.id, campo.mensaje, 'warning')
            return false;
        }

        if (campo.validar && !campo.validar(valor)) {
            mostrarAlertaAlertify(campo.id, campo.mensajeInvalido, 'warning')
            return false;
        }
    }
    return true;
};

//#endregion

//#region init

function inicializarPagina() {

}

// Inicializa Cliente Manager

document.addEventListener('DOMContentLoaded', function () {
    LoginManager.init();
    //inicializarPagina();
});

//#endregion