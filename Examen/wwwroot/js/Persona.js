//#region Selectors

const selectors = {
    idPersona: '#IdPersona',
    nombre: '#txtNombre',
    apellidoPaterno: '#txtApellidoPaterno',
    apellidoMaterno: '#txtApellidoMaterno',   
    identificacion: '#txtIdentificacion',    
    table: '#resultsTable'
};

//#endregion

//#region Persona Manager

const PersonaManager = {
    init: function () {
        const btnAgregar = document.querySelector('#btnGuardar');
        const btnActualizar = document.querySelector('#btnActualizar');
        const btnEliminar = document.querySelector('#confirmBtn');

        if (btnAgregar) btnAgregar.addEventListener('click', this.agregarPersona);
        if (btnActualizar) btnActualizar.addEventListener('click', this.actualizarPersona);
        if (btnEliminar) btnEliminar.addEventListener('click', this.eliminarPersona);
    },

    agregarPersona: function () {
        if (!validarFormulario()) return;

        const data = obtenerDatosFormulario();

        ajaxRequest('/Persona/AgregarPersona', data);
    },

    actualizarPersona: function () {
        if (!validarFormulario()) return;

        const idPersona = $(selectors.idPersona).val();
        const data = {
            ...obtenerDatosFormulario(),
            id: idPersona
        };

        ajaxRequest('/Persona/EditarPersona', data, '/Persona/Index');
    },

    eliminarPersona: function () {
        const idPersona = $(selectors.idPersona).val();

        const data = { idPersona };

        ajaxRequest('/Persona/EliminarPersonaConfirm', data, '/Persona/Index');
    }
};

//#endregion

//#region Funciones

const obtenerDatosFormulario = () => {
    return {
        nombre: $(selectors.nombre).val(),
        apellidoPaterno: $(selectors.apellidoPaterno).val(),
        apellidoMaterno: $(selectors.apellidoMaterno).val(),
        identificacion: $(selectors.identificacion).val()        
    };
};

//#endregion

//#region Mostrar Modal Confirmacion

function mostrarModalConfirmacionPersona() {
    $('#modalNombrePersona').text($(selectors.nombre).val());
    $('#modalIdentificacionPersona').text($(selectors.identificacion).val());

    $('#modalConfirmacion').modal('show');
}

//#endregion

//#region Validaciones

const validarFormulario = () => {
    const campos = [
        { id: $(selectors.nombre), mensaje: 'Nombre es requerido' },
        { id: $(selectors.apellidoPaterno), mensaje: 'Apellido paterno es requerido' },        
        //{ id: $(selectors.apellidoMaterno), mensaje: 'Apellido materno es requerido' }        
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
    PersonaManager.init();
    //inicializarPagina();
});

//#endregion
