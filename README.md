# TaskFlow DAM

Aplicación de escritorio desarrollada con WPF y C# para gestionar tareas académicas del ciclo de Desarrollo de Aplicaciones Multiplataforma (DAM).

El proyecto se ha realizado como parte de la práctica **DAM2_DI_UD03_P01**, con especial atención al enlace de datos (*Data Binding*), la actualización reactiva de la interfaz, la usabilidad y la accesibilidad.

## 1. Información del proyecto

* **Nombre:** TaskFlow DAM
* **Ciclo formativo:** Desarrollo de Aplicaciones Multiplataforma (DAM)
* **Módulo:** Desarrollo de Interfaces (DI)
* **Tipo de aplicación:** WPF
* **Lenguaje:** C#
* **Framework objetivo:** .NET 10.0
* **Arquitectura:** separación entre modelo, lógica de presentación e interfaz XAML.

## 2. Objetivo de la aplicación

El objetivo es desarrollar una aplicación de escritorio que permita registrar y consultar tareas académicas, asociarlas a un módulo formativo, establecer prioridades y fechas límite, identificar tareas urgentes y realizar un seguimiento visual del progreso.

La aplicación utiliza el patrón de separación Modelo-Vista-ViewModel (MVVM), mediante una clase encargada de gestionar los datos y la lógica de presentación, conectada con la interfaz a través de enlaces de datos.

## 3. Arquitectura implementada

La aplicación organiza sus responsabilidades en tres partes principales: modelo, lógica de presentación e interfaz gráfica.

### 3.1. Modelo: Models/Tarea.cs

Contiene la clase `Tarea`, que representa cada tarea de la aplicación.

Sus propiedades son:

* `Titulo`: nombre descriptivo de la tarea.
* `Modulo`: asignatura o módulo formativo.
* `FechaLimite`: fecha límite opcional de tipo `DateTime?`.
* `Prioridad`: nivel de prioridad (Baja, Media o Alta).
* `EsUrgente`: indica si la tarea requiere atención prioritaria.
* `Completada`: indica si la tarea se ha terminado.

La clase implementa `INotifyPropertyChanged`, lo que permite notificar a la interfaz cuando cambia alguna de sus propiedades.

De esta forma, cuando una tarea se marca como completada, los elementos enlazados pueden reaccionar al cambio sin necesidad de actualizar manualmente toda la interfaz.

### 3.2. Lógica de presentación: ViewModels/MainViewModel.cs

La clase `MainViewModel` gestiona la colección de tareas y la lógica necesaria para presentar la información.

Sus elementos principales son:

* `ObservableCollection<Tarea> Tareas`: colección observable que almacena las tareas.
* `ProgresoPorcentaje`: calcula el porcentaje de tareas completadas.
* `TextoProgreso`: genera un resumen textual del progreso.
* `TareasPendientesCount`: calcula el número de tareas pendientes.
* `EstadoApp`: almacena el mensaje de estado de la aplicación.
* `Modulos`: proporciona las opciones disponibles en el selector de módulos.
* `PuedeAgregarTarea`: determina si el título introducido permite habilitar el botón de alta.

La colección utiliza `CollectionChanged` para detectar altas y eliminaciones. Además, la clase se suscribe al evento `PropertyChanged` de cada tarea para detectar cambios en su estado de finalización.

Esto permite actualizar automáticamente el progreso y los contadores cuando se modifica la colección o el estado de una tarea.

### 3.3. Enlace de datos: Data Binding

La comunicación entre la interfaz XAML y el `MainViewModel` se realiza mediante Data Binding.

En el constructor de `MainWindow`, se establece el contexto de datos:

`DataContext = new MainViewModel();`

Los controles de la interfaz se enlazan a las propiedades del ViewModel.

Se utilizan diferentes modos de enlace según la necesidad:

* **TwoWay:** permite que los cambios introducidos en los controles se reflejen en las propiedades del ViewModel y viceversa.
* **OneWay:** permite mostrar información calculada en el ViewModel, como el progreso o el número de tareas pendientes.

También se utiliza `UpdateSourceTrigger=PropertyChanged` en el campo del título para actualizar la propiedad `NuevoTitulo` mientras el usuario escribe.

### 3.4. Colecciones y actualización reactiva

La aplicación utiliza `ObservableCollection<Tarea>` para almacenar las tareas.

Esta colección notifica automáticamente los cambios estructurales, como la incorporación o eliminación de elementos.

Por otro lado, `INotifyPropertyChanged` permite detectar modificaciones en las propiedades individuales de cada tarea.

La combinación de ambos mecanismos consigue que la interfaz se mantenga sincronizada con los datos sin tener que reconstruir manualmente la lista o recalcular los indicadores desde la vista.

### 3.5. Interfaz gráfica: MainWindow.xaml

Define la interfaz mediante XAML.

Incluye:

* Cabecera de la aplicación.
* Formulario de creación de tareas.
* Selector de módulo y fecha límite.
* Selección de prioridad y urgencia.
* Indicador de progreso.
* Lista interactiva de tareas.
* Botones para añadir, limpiar y eliminar.
* Barra de estado con información contextual.

### 3.6. Controladores: MainWindow.xaml.cs

Contiene los controladores de eventos de la interfaz y establece el `DataContext` con una instancia de `MainViewModel`.

Los eventos permiten añadir tareas, limpiar el formulario, actualizar la prioridad seleccionada y eliminar tareas.

## 4. Funcionalidades implementadas

### Gestión de tareas

* Crear tareas con título obligatorio.
* Asociar cada tarea a un módulo DAM.
* Establecer una fecha límite opcional.
* Seleccionar una prioridad entre Baja, Media y Alta.
* Marcar tareas como urgentes.
* Marcar tareas como completadas o pendientes.
* Eliminar tareas individualmente.
* Limpiar el formulario sin eliminar las tareas existentes.

### Seguimiento del progreso

La aplicación calcula el porcentaje de tareas completadas respecto al total automáticamente.

La barra de progreso, el texto informativo y el contador de tareas pendientes se actualizan al modificar el estado de la colección.

Si no existen tareas, el porcentaje de progreso es cero.

### Indicadores visuales

* Las tareas completadas muestran el título tachado.
* Las tareas urgentes se identifican mediante una etiqueta destacada.
* El botón de añadir se deshabilita cuando el título está vacío o contiene únicamente espacios.
* La barra de estado informa del estado de la aplicación y del número de tareas pendientes.

## 5. Captura de pantalla de la aplicación final

La siguiente imagen muestra la interfaz de TaskFlow DAM en ejecución después de aplicar las correcciones de usabilidad y accesibilidad.

![Captura de pantalla de TaskFlow DAM](docs/capturas/taskflowDAM-final.png)

## 6. Usabilidad y accesibilidad

La interfaz se ha diseñado teniendo en cuenta criterios de usabilidad y accesibilidad:

* Distribución mediante contenedores `Grid`, `DockPanel`, `StackPanel` y `Border`.
* Márgenes y espacios interiores para separar los elementos.
* Etiquetas con mnemónicos para acceder a los campos mediante el teclado.
* Orden de tabulación configurado mediante `TabIndex`.
* Confirmación mediante `Enter` y limpieza mediante `Esc`.
* Propiedades `AutomationProperties.Name` y `AutomationProperties.HelpText` en los controles relevantes.
* Agrupación semántica de las opciones de prioridad mediante `GroupBox`.
* Áreas de interacción ampliadas en los RadioButton y el CheckBox de urgencia.
* Contraste elevado entre los textos principales y sus fondos.
* Desplazamiento vertical para acceder al contenido cuando no cabe en la ventana.
* Dimensiones mínimas de ventana de 650 × 500 píxeles.

Se han tomado como referencia las heurísticas de Nielsen, ISO 9241-110 y los criterios de accesibilidad WCAG 2.1 Nivel AA.

## 7. Pruebas funcionales realizadas

Durante el desarrollo se han realizado las siguientes comprobaciones manuales:

| Prueba                                   | Resultado                  |
| ---------------------------------------- | -------------------------- |
| Compilación de la solución               | 0 errores y 0 advertencias |
| Creación de tareas                       | Correcto                   |
| Validación del título vacío              | Botón deshabilitado        |
| Validación de títulos con espacios       | Botón deshabilitado        |
| Alta con título válido                   | Correcto                   |
| Limpieza del formulario                  | Correcto                   |
| Selección de módulos                     | Correcto                   |
| Selección de prioridad                   | Correcto                   |
| Selección de fecha límite                | Correcto                   |
| Creación de tareas urgentes              | Correcto                   |
| Identificación visual de urgencias       | Correcto                   |
| Tachado de tareas completadas            | Correcto                   |
| Actualización del progreso               | Correcto                   |
| Actualización del contador de pendientes | Correcto                   |
| Eliminación individual de tareas         | Correcto                   |
| Navegación por teclado                   | Comprobada                 |
| Atajo `Enter`                            | Correcto                   |
| Atajo `Esc`                              | Correcto                   |
| Mnemónicos `Alt`                         | Comprobados                |
| Desplazamiento vertical                  | Correcto                   |
| Apariencia del botón deshabilitado       | Corregida y comprobada     |

Estas pruebas se han realizado manualmente durante el desarrollo. No constituyen una batería automatizada de pruebas funcionales.

## 8. Auditoría de usabilidad y accesibilidad con IA

Durante el desarrollo se realizó una autoauditoría asistida por inteligencia artificial para identificar posibles problemas de usabilidad y accesibilidad antes de la entrega final.

La auditoría tomó como referencia:

* Heurísticas de usabilidad de Nielsen.
* Norma ISO 9241-110.
* Pautas WCAG 2.1 Nivel AA.
* Propiedades de accesibilidad UI Automation.
* Ergonomía de interacción.

### 8.1. Prompt utilizado en el Pre-Test

Se utilizó el siguiente prompt para solicitar a la IA una auditoría de la interfaz y del código XAML:

> Actúa como un Auditor Senior de Usabilidad y Accesibilidad (UX/A11y) conforme a los estándares ISO 9241-110 y WCAG 2.1 (Nivel AA). Audita la interfaz WPF adjunta (imagen y código XAML) de la app TaskFlow DAM.
>
> Evalúa de forma estricta los siguientes cuatro vectores:
>
> **1. Heurísticas de Nielsen (H1 a H10):**
>
> * H1: Visibilidad del estado (ProgressBar reactiva, StatusBar con contadores).
> * H3: Control y libertad (botón cancelar con IsCancel=True, tecla Esc).
> * H4: Consistencia y estándares visuales (márgenes, paleta armónica).
> * H5: Prevención de errores (campos obligatorios protegidos, controles especializados).
> * H7: Flexibilidad y eficiencia (IsDefault=True, mnemónicos Alt+X, TabIndex).
> * H8: Estética y minimalismo (ausencia de saturación, respiro de 10-15 px).
>
> **2. Accesibilidad y UI Automation (UIA):**
>
> * Presencia de AutomationProperties.Name y HelpText en todos los inputs y botones.
> * Vinculación formal de etiquetas mediante Label Target o LabeledBy.
> * Ratio de contraste cromático WCAG (mínimo 4.5:1).
>
> **3. Ergonomía de interacción (ISO 9241-110 / Fitts / Gestalt):**
>
> * Diana táctil y dimensiones mínimas de botones interactivos (mínimo 36-44 px).
> * Agrupación semántica mediante GroupBox o Border (Ley de Región Común).
>
> **4. Matriz de incidencias por severidad (escala de Nielsen 0 a 4):**
>
> * Severidad 4: catastrófico.
> * Severidad 3: grave o crítico.
> * Severidad 2: moderado.
> * Severidad 1: cosmético.
>
> Emite un informe con la tabla de incidencias y una puntuación cuantitativa de 0 a 10.

### 8.2. Informe inicial de la IA

La auditoría inicial identificó oportunidades de mejora en la agrupación semántica de la prioridad, las dimensiones de las áreas interactivas y la validación de accesibilidad mediante tecnologías de asistencia.

La valoración inicial fue de **8,7/10**.

Las incidencias detectadas fueron:

| ID     | Incidencia                                                                       | Severidad inicial |
| ------ | -------------------------------------------------------------------------------- | ----------------: |
| INC-01 | El grupo de prioridad carecía de asociación semántica formal con su encabezado.  |                 2 |
| INC-02 | Los RadioButton no garantizaban una superficie de interacción de 36-44 px.       |                 2 |
| INC-03 | El CheckBox de urgencia no garantizaba una superficie de interacción suficiente. |                 2 |
| INC-04 | Los márgenes principales superaban los 10-15 px orientativos.                    |                 1 |
| INC-05 | Faltaba validar la accesibilidad mediante tecnologías de asistencia.             |                 2 |

### 8.3. Correcciones aplicadas

A partir del informe inicial se realizaron las siguientes modificaciones:

| ID     | Corrección aplicada                                                                          | Resultado         |
| ------ | -------------------------------------------------------------------------------------------- | ----------------- |
| INC-01 | Se sustituyó el encabezado TextBlock por un GroupBox para agrupar las opciones de prioridad. | Resuelta          |
| INC-02 | Se aumentó el Padding y se estableció MinHeight de 44 px en los RadioButton.                 | Resuelta          |
| INC-03 | Se aumentó el Padding y se estableció MinHeight de 44 px en el CheckBox de urgencia.         | Resuelta          |
| INC-04 | Se mantuvieron los márgenes para conservar la separación visual y la legibilidad.            | Aceptada          |
| INC-05 | Se realizaron pruebas con Narrador de Windows y Accessibility Insights FastPass.             | Pruebas superadas |

### 8.4. Validación posterior a las correcciones

Después de aplicar las mejoras se realizaron nuevas comprobaciones:

* **Compilación:** 0 errores y 0 advertencias.
* **Pruebas funcionales:** creación, modificación, finalización y eliminación de tareas.
* **Navegación por teclado:** comprobación de TabIndex, mnemónicos y teclas Enter y Escape.
* **Narrador de Windows:** reconocimiento correcto de los controles y del grupo de prioridad.
* **Accessibility Insights for Windows:** ejecución de FastPass sin incidencias detectadas.
* **Adaptabilidad:** comprobación de diferentes tamaños de ventana y escalados de pantalla.
* **Contraste:** revisión de las principales combinaciones cromáticas.

La valoración técnica final de la interfaz fue de **9,4/10**.

Esta puntuación es orientativa y no constituye una certificación formal de conformidad WCAG 2.1 AA.

### 8.5. Evidencias de la auditoría

Se incluyen las capturas de pantalla que documentan el proceso de autoauditoría:

**Prompt ejecutado en el Pre-Test**

![Captura del prompt de auditoría](docs/capturas/prompt-pretest.png)

**Informe inicial generado por IA**

![Captura del informe de auditoría](docs/capturas/informe-IA-pretest.png)


## 9. Tecnologías utilizadas

* C#
* WPF
* XAML
* .NET 10.0
* `ObservableCollection<T>`
* `INotifyPropertyChanged`
* Data Binding
* Data Templates y Data Triggers
* Propiedades de automatización de WPF

## 10. Ejecución

1. Abrir la solución en Visual Studio compatible con .NET 10.
2. Comprobar que está instalado el SDK de .NET correspondiente.
3. Restaurar las dependencias del proyecto si fuera necesario.
4. Compilar la solución.
5. Ejecutar el proyecto WPF.

## 11. Conclusión

TaskFlow DAM permite gestionar tareas académicas mediante una interfaz de escritorio con actualización reactiva, validación de entradas e indicadores visuales del progreso.

Durante el desarrollo se ha trabajado tanto en la funcionalidad como en la usabilidad y accesibilidad de la aplicación.

La autoauditoría asistida por IA permitió identificar aspectos mejorables, aplicar correcciones y realizar pruebas posteriores con herramientas de accesibilidad.

El resultado es una aplicación organizada, funcional y diseñada teniendo en cuenta criterios de usabilidad y accesibilidad.
