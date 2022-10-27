mod drawable;
mod rect;
mod renderer;
mod scene;
mod vertex;
mod window;
mod world;

use window::run;

fn main() {
    pollster::block_on(run());
}
