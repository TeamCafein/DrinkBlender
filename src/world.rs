use liquidfun::box2d::{
    collision::shapes::polygon_shape::PolygonShape,
    common::math::Vec2,
    dynamics::{
        body::{BodyDef, BodyType},
        fixture::FixtureDef,
        world::World,
    },
};

use crate::rect::Rect;

pub fn create_world() -> World {
    let mut world = World::new(&Vec2::new(0.0, -10.0));

    let ground = Rect::new(&mut world, 0., -10., 50., 10.);

    let mut body_def = BodyDef::default();
    body_def.body_type = BodyType::DynamicBody;
    body_def.position.set(0.0, 4.0);

    let body = world.create_body(&body_def);

    let mut dynamic_box = PolygonShape::new();
    dynamic_box.set_as_box(1.0, 1.0);

    let mut fixture_def = FixtureDef::new(&dynamic_box);
    fixture_def.density = 1.0;
    fixture_def.friction = 0.3;

    let time_step = 1.0 / 60.0;
    let velocity_iterations = 6;
    let position_iterations = 2;

    world
}
