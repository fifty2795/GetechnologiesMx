//#region Selectors

const selectors = {    
    monto: '#txtMonto',
    ddlPersona: '#ddlPersona',    
    table: '#resultsTable'
};

//#endregion

//#region Factura Manager

const FacturaManager = {
    init: function () {
        const btnAgregar = document.querySelector('#btnGuardar');        

        if (btnAgregar) btnAgregar.addEventListener('click', this.agregarFactura);        
    },

    agregarFactura: function () {
        if (!validarFormulario()) return;

        const data = obtenerDatosFormulario();

        ajaxRequest('/Factura/AgregarFactura', data);
    }   
};

//#endregion

//#region Funciones

const obtenerDatosFormulario = () => {
    return {
        monto: $(selectors.monto).val(),
        idPersona: $(selectors.ddlPersona).val()        
    };
};

//#endregion

//#region Validaciones

const validarFormulario = () => {
    const campos = [
        { id: $(selectors.monto), mensaje: 'Monto es requerido' },
        { id: $(selectors.ddlPersona), mensaje: 'Selecciona una persona' }        
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
    FacturaManager.init();
    //inicializarPagina();
});

//#endregion
