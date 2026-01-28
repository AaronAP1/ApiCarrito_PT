# 🛒 API Carrito de Compras – Prueba Técnica Backend (.NET)

Este proyecto implementa una API REST para la gestión de un carrito de compras, desarrollada como parte de una prueba técnica backend en .NET 8, aplicando principios de Clean Architecture, SOLID y buenas prácticas de ingeniería de software.

---

## 🚀 Tecnologías utilizadas

- .NET 8
- ASP.NET Core Web API
- xUnit (tests unitarios)
- FluentAssertions
- Swagger / OpenAPI
- Persistencia en memoria (InMemory)
- Arquitectura Clean / Hexagonal

---

## 📐 Arquitectura

La solución está organizada en capas:

ApiCarrito_PT
│
├── Controllers        → Endpoints HTTP
├── Application        → Casos de uso, servicios, validaciones
├── Domain             → Entidades y reglas de negocio
├── Infrastructure     → Persistencia y catálogos en memoria
└── Tests              → Tests unitarios (xUnit)

Esto permite:
- Bajo acoplamiento
- Alta mantenibilidad
- Facilidad para cambiar infraestructura

---

## 📌 Endpoints disponibles

GET /cart  
POST /cart/items  
PUT /cart/items/{itemId}  
DELETE /cart/items/{itemId}

---

## ✅ Requisitos funcionales cubiertos

- Añadir producto al carrito
- Actualizar producto existente
- Aumentar / disminuir cantidad
- Eliminar producto del carrito
- Obtener contenido del carrito
- Validaciones de negocio por grupos y atributos

---

## 🧪 Tests unitarios

Se implementaron tests unitarios para:

ProductSelectionValidator:
- Grupos requeridos
- Reglas EQUAL_THAN
- Límite máximo de atributos

CartService:
- Producto inexistente (404)
- Reglas inválidas (422)
- Flujo correcto de agregado (201)

Ejecutar tests:
dotnet test

---

## 🧠 Persistencia

 La arquitectura permite reemplazar fácilmente por Redis, MongoDB u otra base de datos

 El sistema valida automáticamente:

| Regla | Descripción |
|-------|-------------|
| **REQUIRED** | Grupos obligatorios deben estar presentes |
| **EQUAL_THAN** | Número exacto de selecciones requeridas |
| **AT_LEAST** | Mínimo de selecciones requeridas |
| **UP_TO** | Máximo de selecciones permitidas |
| **MaxQuantity** | Cantidad máxima por atributo individual |
| **Duplicados** | Prevención de grupos o atributos duplicados |

## 📚 Principios SOLID Aplicados

- **S**ingle Responsibility: Cada clase tiene una única responsabilidad
- **O**pen/Closed: Extensible mediante interfaces
- **L**iskov Substitution: Implementaciones intercambiables vía DI
- **I**nterface Segregation: Interfaces específicas y cohesivas
- **D**ependency Inversion: Dependencia de abstracciones, no implementaciones

---

## ▶️ Ejecución del proyecto

1. Restaurar dependencias:
dotnet restore

2. Ejecutar la API:
dotnet run

3. Swagger:
https://localhost:{puerto}/swagger

---

Autor:
AaronDev
