cmake_minimum_required(VERSION 3.20)
project(SSAOExample)

## Find dependencies
find_package(glfw3 REQUIRED)
find_package(glad REQUIRED)
find_package(glm REQUIRED)
find_package(freetype REQUIRED)
find_package(imgui REQUIRED)
find_package(tinyobjloader REQUIRED)
find_package(assimp REQUIRED)

set (CMAKE_C_COMPILER gcc)
set (CMAKE_CXX_COMPILER g++)
set (CMAKE_CXX_STANDARD 20)

# enable asan
# add_link_options(-fsanitize=address -ggdb)
# add_compile_options(-fsanitize=address -ggdb)

# add_compile_options(-ggdb -O3)
# add_link_options(-ggdb -O3)
# add_compile_options(-Wall -Wextra -Wpedantic)


## Create main executable

file(GLOB main_src
    "src/*.cpp"
    "src/*/*.cpp"
    "src/*/*/*.cpp"
    "src/*/*/*/*.cpp"
    "src/*/*/*/*/*.cpp"
    "src/*/*/*/*/*/*.cpp"
)

file(GLOB shaders
    "src/*.vs"
    "src/*.fs"
    "src/*.tcs"
    "src/*.tes"
    "src/*.gs"
    "src/*.cs"
)

add_executable(main 
  ${main_src}
)

foreach(SHADER ${shaders})
    add_custom_command(
        TARGET main POST_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy ${SHADER} ${CMAKE_SOURCE_DIR}/build)
endforeach(SHADER)

target_include_directories(main PRIVATE .)
target_include_directories(main 
  PRIVATE 
    ${STB_INCLUDE_DIRS}
    "${CMAKE_CURRENT_LIST_DIR}/source"
    "${CMAKE_CURRENT_LIST_DIR}/includes"
)

find_package(OpenGL REQUIRED)
include_directories(${OPENGL_INCLUDE_DIRS})

configure_file(configuration/root_directory.h.in configuration/root_directory.h)
include_directories(${CMAKE_BINARY_DIR}/configuration)

target_link_libraries(
  main
  PRIVATE
    freetype
    glfw
    OpenGL::GL
    glad::glad
    glm::glm
    imgui::imgui
    assimp::assimp
)