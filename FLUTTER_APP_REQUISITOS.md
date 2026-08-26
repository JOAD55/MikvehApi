# Requisitos para la app móvil (Flutter) — MikvehApi

Este documento describe qué necesita la app Flutter para consumir MikvehApi: autenticación, permisos por rol, pantallas sugeridas, modelos (DTOs) y detalles técnicos a tener en cuenta. Refleja el estado real de la API tras agregar autenticación JWT y las restricciones por rol (rama `Desarrollo`, sin push todavía).

## 1. Autenticación

La API usa JWT (Bearer token). No hay cookies ni sesión de servidor: cada request protegido debe incluir `Authorization: Bearer <token>`.

### Flujo

1. `POST /api/auth/login` con `usuario` y `contrasena` → devuelve el token, su expiración y los datos del trabajador.
2. La app guarda el token (recomendado: `flutter_secure_storage`, no `SharedPreferences` en texto plano).
3. Cada request subsiguiente manda el header `Authorization: Bearer <token>`.
4. Al abrir la app con un token guardado, llamar `GET /api/auth/me` para validar que sigue vigente y refrescar los datos del usuario en memoria (evita pedir login de nuevo si el token no expiró).
5. Si cualquier request devuelve `401`, el token expiró o es inválido → limpiar sesión local y mandar a Login.

### Duración del token

`Jwt:ExpiresInMinutes = 480` (8 horas, configurable en el backend). **No hay refresh token todavía** — cuando expira, el usuario debe volver a loguearse. Si en el uso real esto resulta molesto, hay que pedir que se agregue refresh token (no es trivial, es trabajo aparte).

### Endpoints de auth

| Método | Ruta | Auth | Body | Respuesta |
|---|---|---|---|---|
| POST | `/api/auth/login` | Ninguna | `LoginDto` | `TokenResponseDto` (200) / 401 si credenciales inválidas |
| GET | `/api/auth/me` | Bearer | — | `TrabajadorDto` (200) |
| PUT | `/api/auth/cambiar-password` | Bearer | `CambiarPasswordDto` | 204 / 400 si la contraseña actual no coincide |

### Cuenta inicial

La base de datos trae sembrado (vía migración) un usuario administrador para poder emitir el primer token:

- Usuario: `admin`
- Contraseña: `Admin123!`

**Cambiar esta contraseña la primera vez que se use** (`PUT /api/auth/cambiar-password`), y desde ahí crear el resto de trabajadores desde un usuario Administrador.

## 2. Roles y permisos

Hoy la tabla `Roles` es libre (el Administrador puede crear los que quiera desde la app: "Terapeuta", "Recepcionista", etc.), pero **la API solo distingue dos niveles de permiso**: el rol `"Administrador"` (exacto, sensible a mayúsculas) y "todos los demás". No hay una tercera categoría con permisos intermedios todavía.

Si más adelante se necesitan roles con reglas distintas entre sí (p. ej. que un "Recepcionista" pueda ver contabilidad pero no crear trabajadores), eso requiere trabajo adicional en el backend — avisar antes de asumir que ya existe.

### Qué puede hacer cada nivel

| Acción | Administrador | Otro rol autenticado |
|---|---|---|
| Ver citas, clientes, servicios, paquetes, trabajadores | ✅ | ✅ |
| Agendar (crear) citas | ✅ | ✅ |
| Modificar/cancelar **cualquier** cita | ✅ | ❌ |
| Modificar/cancelar una cita **propia** (`trabajadorId` = él mismo) y **futura** | ✅ | ✅ |
| Modificar/cancelar una cita propia ya pasada | ✅ | ❌ (403) |
| Agregar/quitar servicios o paquetes de una cita | mismas reglas que modificar la cita | mismas reglas que modificar la cita |
| Crear/editar/borrar clientes | ✅ | ✅ |
| Crear/editar/borrar servicios y paquetes | ✅ | ✅ (ver nota abajo) |
| Ver estadísticas / contabilidad | ✅ | ❌ (403) |
| Crear/editar/borrar trabajadores | ✅ | ❌ (403) |
| Crear/editar/borrar roles | ✅ | ❌ (403) |
| Cambiar su propia contraseña | ✅ | ✅ |

**Nota:** hoy cualquier trabajador autenticado puede crear/editar/borrar Servicios y Paquetes (no solo Admin) — son catálogos compartidos, no datos "de una persona". Si en la práctica solo el Administrador debe tocar precios/catálogo, se puede restringir en la app (ocultar esos botones a no-admins) y opcionalmente pedir que se refuerce también en el backend.

**Cómo saber el rol en la app:** el JWT trae un claim de rol (`role`) con el nombre exacto del rol (p. ej. `"Administrador"`). También viene en `TrabajadorDto.rolNombre` al hacer login o `GET /api/auth/me`. La app debe guardar `rolNombre` (o al menos un booleano `esAdministrador = rolNombre == "Administrador"`) para decidir qué mostrar.

**Importante:** estas reglas ya están validadas en el backend (probadas de punta a punta), no son solo cosméticas — ocultar un botón en la UI es para UX, pero aunque alguien fuerce el request, el servidor devuelve 403. La app **debe manejar el 403** con un mensaje claro ("No tenés permiso para esto") en vez de crashear.

## 3. Pantallas sugeridas

Basado en los endpoints disponibles y las reglas de permisos:

| Pantalla | Quién la ve | Notas |
|---|---|---|
| **Login** | Todos | usuario + contraseña |
| **Inicio / Agenda del día** | Todos | citas de hoy (`GET /api/citas/semana` filtrado, o construir con `/api/citas/periodo`) |
| **Agenda (semana/mes/futuras)** | Todos | usar `/api/citas/semana`, `/mes`, `/futuras`, `/periodo` |
| **Detalle de cita** | Todos (ver) | mostrar `DetailCitaDto`: cliente, trabajador, detalles (servicios/paquetes), total |
| **Nueva cita** | Todos | `POST /api/citas` |
| **Editar / cancelar cita** | Todos, pero el botón solo debe **habilitarse** si `esAdministrador \|\| (cita.trabajadorId == miId && cita.fechaHoraCita > ahora)` | igual el backend valida, pero evitar mostrar el botón habilitado si va a fallar |
| **Agregar servicio/paquete a una cita** | mismas reglas que editar la cita | `POST /api/citas/detalles` |
| **Clientes (lista + búsqueda)** | Todos | `GET /api/clientes` no pagina, filtrar en el cliente si la lista crece |
| **Nuevo/editar cliente** | Todos | |
| **Servicios** | Todos (ver); crear/editar según se decida (ver nota de la sección 2) | |
| **Paquetes** | Todos (ver); crear/editar según se decida | incluye armar el paquete con `POST /api/paquetes/detalles` |
| **Trabajadores (lista)** | Todos (ver) | `GET /api/trabajadores` |
| **Crear/editar/borrar trabajador** | Solo Administrador | ocultar por completo si `!esAdministrador` |
| **Roles** | Solo Administrador | CRUD completo |
| **Estadísticas / Contabilidad** | Solo Administrador | ver sección 5, ocultar del menú si `!esAdministrador` |
| **Mi perfil** | Todos | `GET /api/auth/me`, botón "cambiar contraseña" |

Nota: no existe endpoint para que un trabajador edite sus propios datos de contacto (teléfono/email) — `PUT /api/trabajadores/{id}` es solo-Administrador. Si se quiere autoservicio de perfil (además de cambiar contraseña), hay que pedirlo como feature aparte.

## 4. Modelos (DTOs)

Formato JSON tal cual los devuelve/espera la API (nombres de campo en camelCase por la serialización default de ASP.NET Core). Generar las clases Dart correspondientes (por ejemplo con `json_serializable`) a partir de esto.

### Auth

```jsonc
// LoginDto (request)
{ "usuario": "string", "contrasena": "string" }

// TokenResponseDto (response de login)
{
  "token": "string",
  "expiraEn": "2026-08-27T02:56:10.311Z",
  "trabajador": { /* TrabajadorDto, ver abajo */ }
}

// CambiarPasswordDto (request)
{ "contrasenaActual": "string", "contrasenaNueva": "string (min 6)" }
```

### Trabajador

```jsonc
// TrabajadorDto (response)
{
  "trabajadorId": 1,
  "nombre": "string",
  "apellidos": "string",
  "usuario": "string",
  "telefono": "string|null",
  "email": "string|null",
  "fechaNacimiento": "2000-01-01|null",
  "rolId": 1,
  "rolNombre": "Administrador"
}

// CreateTrabajadorDto (request, solo Administrador)
{
  "nombre": "string",        // requerido, max 255
  "apellidos": "string",     // requerido, max 255
  "usuario": "string",       // requerido, max 100, se guarda en minúsculas
  "contrasena": "string",    // requerido, max 100
  "telefono": "string|null", // formato teléfono
  "email": "string|null",    // formato email
  "fechaNacimiento": "datetime|null",
  "rolId": 1                 // requerido, > 0
}

// UpdateTrabajadorDto (request, solo Administrador) — todos los campos opcionales,
// solo se sobrescribe lo que venga no-nulo
{ "nombre": null, "apellidos": null, "telefono": null, "email": null, "fechaNacimiento": null, "rolId": null }
```

### Rol

```jsonc
// RolDto
{ "rolId": 1, "nombre": "Administrador", "descripcion": "string" }

// CreateRolDto / UpdateRolDto
{ "nombre": "string (max 50)", "descripcion": "string (max 500)" }
```

### Cliente

```jsonc
// ClienteDto
{
  "clienteId": 1, "nombre": "string", "apellidos": "string",
  "telefono": "string|null", "email": "string|null", "fechaNacimiento": "date|null"
}

// CreateClienteDto / UpdateClienteDto — mismos campos, Update con todo opcional
{ "nombre": "string", "apellidos": "string", "telefono": "string|null", "email": "string|null", "fechaNacimiento": "date|null" }
```

### Servicio / Paquete

```jsonc
// ServicioDto
{ "servicioId": 1, "nombre": "string", "descripcion": "string|null", "duracionMinutos": 30, "precioBase": 100.00 }

// CreateServicioDto / UpdateServicioDto (Update con todo opcional)
{ "nombre": "string", "descripcion": "string|null", "duracionMinutos": 30, "precioBase": 100.00 }

// PaqueteDto
{ "paqueteId": 1, "nombre": "string", "descripcion": "string|null", "precio": 250.00 }

// CreatePaqueteDto / UpdatePaqueteDto
{ "nombre": "string", "descripcion": "string|null", "precio": 250.00 }

// DetallePaqueteDto (un servicio dentro de un paquete)
{ "detallePaqueteId": 1, "nombrePaquete": "string", "nombreServicio": "string" }

// CreateDetallePaqueteDto — agrega un servicio a un paquete
{ "paqueteId": 1, "servicioId": 1 }
```

### Cita (lo más importante)

```jsonc
// CitaDto — lo que devuelven las listas (GET /api/citas, /futuras, /semana, /mes, /periodo)
{
  "citaId": 1,
  "fechaHoraCita": "2026-09-01T10:00:00",   // sin zona horaria, ver seccion 6
  "descripcion": "string|null",
  "totalPagar": 200.00,
  "clienteId": 8,
  "clienteNombre": "string",
  "clienteApellidos": "string",
  "trabajadorId": 30,          // null si no está asignada a nadie
  "trabajadorNombre": "string"
}

// DetailCitaDto — lo que devuelve GET /api/citas/{id} y /con-detalles (con el detalle de servicios/paquetes)
{
  "citaId": 1,
  "fechaHoraCita": "2026-09-01T10:00:00",
  "descripcion": "string|null",
  "totalPagar": 200.00,
  "cliente": { "clienteId": 8, "nombreCompleto": "string" },
  "trabajador": { "trabajadorId": 30, "nombreCompleto": "string" } | null,
  "detallesCita": [ /* DetalleCitaDto[] */ ]
}

// DetalleCitaDto — un servicio o paquete agregado a una cita
{
  "detalleCitaId": 1,
  "citaId": 1,
  "cantidad": 2,
  "subtotal": 200.00,
  "servicio": { "servicioId": 4, "nombre": "string", "precioBase": 100.00 } | null,
  "paquete": { "paqueteId": 1, "nombre": "string", "precio": 250.00 } | null
  // exactamente uno de "servicio"/"paquete" viene no-nulo
}

// CreateCitaDto (request)
{
  "fechaHoraCita": "2026-09-01T10:00:00", // requerido
  "descripcion": "string|null",
  "clienteId": 8,                          // requerido, > 0
  "trabajadorId": 30                       // opcional, puede quedar sin asignar
}

// UpdateCitaDto (request) — todo opcional, solo sobrescribe lo enviado
{ "fechaHoraCita": null, "descripcion": null, "trabajadorId": null }

// CreateDetalleCitaDto (request) — agrega un servicio O un paquete a una cita existente
{
  "citaId": 1,
  "servicioId": 4,   // exactamente uno de estos dos, no ambos ni ninguno
  "paqueteId": null,
  "cantidad": 1
}
```

### Estadísticas (solo Administrador)

```jsonc
// ResumenEstadisticasDto — GET /api/estadisticas/resumen
{
  "inicio": "2026-08-01T00:00:00",
  "fin": "2026-08-31T00:00:00",
  "totalCitas": 42,
  "totalIngresos": 8400.00,
  "promedioPorCita": 200.00,
  "ingresosPorTrabajador": [
    { "trabajadorId": 30, "nombreCompleto": "Tera Peuta", "totalCitas": 20, "totalIngresos": 4000.00 },
    { "trabajadorId": null, "nombreCompleto": "Sin asignar", "totalCitas": 1, "totalIngresos": 0.00 }
  ],
  "serviciosMasSolicitados": [
    { "nombre": "Inmersión", "tipo": "Servicio", "cantidadSolicitada": 30, "totalIngresos": 3000.00 },
    { "nombre": "Paquete completo", "tipo": "Paquete", "cantidadSolicitada": 5, "totalIngresos": 1250.00 }
  ]
}
```

## 5. Referencia completa de endpoints

Base URL: `/api`. Todos requieren `Authorization: Bearer <token>` salvo `POST /api/auth/login`.

| Recurso | Método | Ruta | Rol requerido |
|---|---|---|---|
| **Auth** | POST | `/auth/login` | ninguno |
| | GET | `/auth/me` | cualquiera |
| | PUT | `/auth/cambiar-password` | cualquiera |
| **Citas** | GET | `/citas` | cualquiera |
| | GET | `/citas/{id}` | cualquiera |
| | GET | `/citas/con-detalles` | cualquiera |
| | GET | `/citas/futuras` | cualquiera |
| | GET | `/citas/semana?fecha=` | cualquiera |
| | GET | `/citas/mes?fecha=` | cualquiera |
| | GET | `/citas/periodo?inicio=&fin=` | cualquiera |
| | GET | `/citas/detalles/{id}` | cualquiera |
| | POST | `/citas` | cualquiera |
| | POST | `/citas/detalles` | propia+futura, o Admin |
| | PATCH | `/citas/{id}` | propia+futura, o Admin |
| | DELETE | `/citas/{id}` | propia+futura, o Admin |
| | DELETE | `/citas/detalles/{id}` | propia+futura, o Admin |
| **Clientes** | GET/POST/PATCH/DELETE | `/clientes[/{id}]` | cualquiera |
| **Servicios** | GET/POST/PUT/DELETE | `/servicios[/{id}]` | cualquiera |
| **Paquetes** | GET/POST/PUT/DELETE | `/paquetes[/{id}]` | cualquiera |
| | GET/POST/DELETE | `/paquetes/detalles[/{id}]` | cualquiera |
| **Trabajadores** | GET | `/trabajadores`, `/trabajadores/{id}`, `/trabajadores/by-user/{user}` | cualquiera |
| | POST/PUT/DELETE | `/trabajadores[/{id}]` | **Administrador** |
| **Roles** | GET/POST/PATCH/DELETE | `/roles[/{id}]` | **Administrador** |
| **Estadísticas** | GET | `/estadisticas/resumen?inicio=&fin=` | **Administrador** |

## 6. Detalles técnicos importantes para la app

- **Fechas sin zona horaria:** `FechaHoraCita` se guarda y devuelve como `datetime` de SQL Server sin offset (ej. `"2026-09-01T10:00:00"`, sin `Z` ni `+00:00`). Si el `DateTime.parse` de Dart lo interpreta como UTC vs. local puede desfasar la hora mostrada. Tratarlo como **hora local del negocio**, no como UTC, y no aplicar conversión de zona horaria al mostrarlo.
- **Formato de errores:**
  - Errores de validación de modelo (`[Required]`, `[MaxLength]`, etc.) → 400 con el formato estándar de ASP.NET Core (`{ "errors": { "campo": ["mensaje"] }, "title": "...", "status": 400 }`).
  - Errores de negocio (permiso denegado, usuario duplicado, etc.) → `ProblemDetails`: `{ "title": "...", "status": 403|409|500, "detail": "mensaje legible", "instance": "/api/..." }`. Mostrar `detail` al usuario cuando exista.
  - `404` simple sin body cuando un recurso no existe.
- **CORS:** solo aplica si se corre una versión Flutter **Web** contra la API en desarrollo — hay que agregar el origen (`http://localhost:xxxx`) a `Cors:AllowedOrigins` en `appsettings.Development.json`. No afecta a la app en Android/iOS nativos.
- **Conexión desde el emulador/dispositivo:**
  - Emulador Android → la API en `localhost` de la PC se accede como `http://10.0.2.2:<puerto>`.
  - Simulador iOS → `http://localhost:<puerto>` funciona directo.
  - Dispositivo físico → usar la IP LAN de la PC, y que el firewall permita el puerto.
  - En producción, usar HTTPS con certificado válido (revisar `MikvehApi/appsettings.Production.json`, que actualmente no trae connection string — hay que configurarla por variable de entorno al desplegar).
- **Sin paginación:** ningún listado (`GetAll`) pagina todavía. Con pocos cientos de registros no es problema, pero si la base crece hay que pedir que se agregue paginación antes de que la lista de clientes/citas se vuelva lenta de cargar en el celular.
- **Sin recuperación de contraseña:** si un trabajador olvida su contraseña, hoy solo un Administrador puede "resetearla" indirectamente... en realidad tampoco existe ese endpoint — actualmente la única forma es que un Administrador la reescriba directamente en la base de datos, o se agregue un endpoint de reseteo (pendiente, no implementado).
- **Manejo de sesión expirada:** interceptar globalmente las respuestas 401 (p. ej. con un interceptor de `dio`) para limpiar el token guardado y redirigir a Login, en vez de manejarlo pantalla por pantalla.
- **Confirmaciones destructivas:** pedir confirmación antes de cancelar/borrar una cita o borrar un cliente/trabajador — la API borra sin posibilidad de deshacer.

## 7. Pendientes conocidos (no implementados, quedan para después si se necesitan)

- Refresh token (hoy expira y obliga a re-login).
- Recuperación de contraseña por email.
- Autoservicio de perfil (editar teléfono/email propios sin ser Administrador).
- Paginación en los listados.
- Roles con permisos intermedios entre "Administrador" y "todos los demás".
- Notificaciones push para citas próximas.
