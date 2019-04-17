export const UsuarioAutenticado = () => localStorage.getItem("userautent-token-spmedicalgroup") != null;

export const parseJwt = () => {
    var base64Url = localStorage.getItem("userautent-token-spmedicalgroup").split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');

    return JSON.parse(window.atob(base64));
}