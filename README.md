# RPG-POKEMON-DEMO
Un juego RPG demo siguiendo la temática de pokemon

# Links
=====
- [Github](https://github.com/HenryVB/RPG-POKEMON-DEMO)
- [Itch.io](https://henryvb.itch.io/pokemon-rpg)

## Sobre el juego
=============
Simulación de una parte del juego de Pokémon.

## Misiones u objetivos
===================
Se deben cumplir 3 misiones:
1. Obtener tu Pokémon inicial.
2. Ayudar a los NPCs a darles su objeto que buscan (área 2 lado izquierdo).
3. Pelear con 2 Pokémon salvajes en la hierba y ganarles (área lado derecho). Puedes intentar huir de la batalla si ves que no es conveniente por desventaja de tipos de Pokémon.

### IMPORTANTE:
=====
- Es importante hablar con los NPCs para tener contexto de lo que se está haciendo.
- En caso tu Pokémon llegue a ser derrotado perderás el juego y volverás a hacer todo de inicio (game over).
- En caso logres cumplir los 3 objetivos habrás ganado el juego.
- Los objetivos son de manera secuencial.

## Controles:
=========
- **Enter:** Para abrir menú.
- **Z:** Para hablar con NPCs o para elegir una opción en el menú de batallas.
- **X:** Cancelar alguna acción como al momento de interactuar en el menú de batallas o salir del inventario.
- **Movimiento:** Flechas arriba (W), abajo (S), derecha (D), izquierda (A): Para desplazamiento.

## Features
=======
- Sistema de batalla por turnos básico.
- Diálogo con NPCs.
- Cambios de escenas (ingreso a casas otras rutas).
- Menú (solo funciona la mochila de inventario en modo visualización de cuántos ítems tengo - BAG). El resto de elementos en el menú solo son visuales por esta vez.
- Interacción con objetos (recoger).
- Sonidos y FX.
- Animaciones.
- Tiles y escenarios.
- Misiones (3) a cumplir.
- Menús de inicio, game over y victoria.
- Si tu Pokémon muere, pierdes el juego y vuelves a empezar.
- No se manejan características adicionales a las que se mencionan.

## Alcance del Juego
===============
- Para el sistema de batallas solo se manejan movimientos de ataque (2 ataques máximo) sin efectos secundarios (no se usan movimientos para afectar estadísticas ni estado).
- Para el sistema de batallas solo se pueden utilizar las opciones de batallas (batallas y salir). El resto de opciones solo son visuales.
- Las batallas se ven afectadas solo por factores de efectividad de tipos, golpes críticos, velocidad del Pokémon más rápido para atacar primero.
- Solo se considera que el jugador tenga 1 solo Pokémon para esta oportunidad (no un party/equipo).
- Se fabricaron algunos ítems que son las pociones y ether. No se podrán utilizar por el momento, estos solo podrán recogerse o entregarse. No se utilizaron HM o TM especiales u otros tipos de ítems.
- Solo se fabricaron 3 Pokémon para esta oportunidad: Bulbasaur, Charmander, Squirtle.
